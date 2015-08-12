// The MIT License (MIT)
//
// Copyright (c) 2015 FPT Software
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using ModernHttpClient;
using Cirrious.MvvmCross.ViewModels;
using FSoft.WordApp.Core.Services;
using FSoft.WordApp.Core.Models;
using System.Collections.ObjectModel;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;
using Cirrious.CrossCore;
using Beezy.MvvmCross.Plugins.SecureStorage;

namespace FSoft.WordApp.Core.ViewModels
{
	public class LoginViewModel: BaseViewModel
    {
		public LoginViewModel(IFNewsService service, IMvxDeviceInfo deviceInfoPlugin) : base(service, deviceInfoPlugin)
        {
			#if DEBUG
//			Username = "admin";
//			Password= "123456";
			#endif

			//IsLoading = false;
			try {
				LoadSavedData();
			} catch (Exception e ){
			}
        }

		private void LoadSavedData() {
			var data = Mvx.Resolve<IMvxProtectedData>();
			var _u = data.Unprotect (UsernamePropertyName);
			var _p = data.Unprotect (PasswordPropertyName);
			var _r = data.Unprotect (RememberPasswordPropertyName);

			if (!string.IsNullOrEmpty (_u))
				Username = _u;
			if (!string.IsNullOrEmpty (_p))
				Password = _p;
			if (!string.IsNullOrEmpty (_r)) {
				RememberPassword = _r.Equals ("1");
			}
		}

		private void SaveData() {
			var data = Mvx.Resolve<IMvxProtectedData>();
			if (RememberPassword) {
				data.Protect (UsernamePropertyName, Username);
				data.Protect (PasswordPropertyName, Password);
				data.Protect (RememberPasswordPropertyName, "1");
			} else {
				data.Protect (RememberPasswordPropertyName, "0");
				data.Remove (UsernamePropertyName);
				data.Remove (PasswordPropertyName);
			}
		}

        public const string UsernamePropertyName = "Username";
        private string username = string.Empty;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value, UsernamePropertyName); }
        }

        public const string PasswordPropertyName = "Password";
        private string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value, PasswordPropertyName); }
        }

		public const string RememberPasswordPropertyName = "RememberPassword";
		private bool _RememberPassword = false;
		public bool RememberPassword
		{
			get { return _RememberPassword; }
			set { SetProperty(ref _RememberPassword, value, RememberPasswordPropertyName); }
		}

		public const string ErrorPropertyName = "Error";
		private string error = string.Empty;
		public string Error {
			get { return error; }
			set { 
				SetProperty (ref error, value, ErrorPropertyName);
				HasError = !string.IsNullOrEmpty (value);
				if (HasError)
					ShowErrorMessage (Error);
			}
		}

		private bool _HasError;
		public bool HasError {
			get { return _HasError; }
			set { SetProperty (ref _HasError, value, "HasError");}
		}

		private bool isLoading = false;
		public new bool IsLoading {
			get { return isLoading; }
			set { 
				SetProperty (ref isLoading, value, IsLoadingPropertyName);
				IsNotBusy = !(value);
				ShowIntro = false;
			}
		}
        
		private bool isNotBusy = true;
		public bool IsNotBusy {
			get { return isNotBusy;}
			set { SetProperty (ref isNotBusy, value, "IsNotBusy"); }
		}


		private bool _IsSoftkeyHided;
		public bool IsSoftkeyHided {
			get { return _IsSoftkeyHided;}
			set { 
				SetProperty (ref _IsSoftkeyHided, value, "IsSoftkeyHided");
				ShowIntro = value;
				if (IsLoading)
					ShowIntro = false;
			}
		}

		private bool _IsSoftkeyShowed;
		public bool IsSoftkeyShowed {
			get { return _IsSoftkeyShowed;}
			set { 
				SetProperty (ref _IsSoftkeyShowed, value, "IsSoftkeyShowed");
				//ShowIntro = value;//dont call here!!!!
			}
		}

		private string _Info = string.Empty;
		public string Info {
			get { return _Info; }
			set { SetProperty (ref _Info, value, "Info");}
		}

		private bool _showIntro;
		public bool ShowIntro {
			get { return _showIntro;}
			set { SetProperty (ref _showIntro, value, "ShowIntro");}
		}

		private bool _useTreeMenu = true;
		public bool UseTreeMenu {
			get { return _useTreeMenu; }
			set { SetProperty (ref _useTreeMenu, value, "UseTreeMenu"); }
		}
		private Cirrious.MvvmCross.ViewModels.MvxCommand _loginCommand;
		public System.Windows.Input.ICommand LoginCommand
		{
			get
			{
				_loginCommand = _loginCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoLogin);
				return _loginCommand;
			}
		}
		public event EventHandler UpdateClientHandler;
		public void showUpdateStore(){
			ShowWebPage(Settings.UpdateInfo.Update_info.Link);
		}

		async private void DoLogin()
		{
			if (Status == NetworkStatus.NotReachable) {
				Error = Settings.MSG_NETWORK_NOT_REACHABLE;
				return;
			}

			SaveData ();
			await ExecuteLoginCommand ();

			if (Settings.UpdateInfo != null) {
				if (UpdateClientHandler != null)
					UpdateClientHandler (this, new UpdateClientEventArgs("Update",Settings.UpdateInfo.Update_info.Message,"OK"));
			} else if (Settings.wpLoggedIn) {
				Settings.Categories = null; //force to refresh
				Close (this);
				Settings.RootViewModel.RefreshData ();
				//ShowViewModel<RootViewModel>();
			}
		}

		protected async Task ExecuteLoginCommand()
		{
			Error = string.Empty;

			if (Username.Equals(string.Empty) || Password.Equals(string.Empty)) {
				Error = Settings.MSG_EMPTY_USERNAME_OR_PWD;
				return;
			}

			if (isLoading)
				return;

			IsLoading = true;
			Info = "We're processing...";

			Settings.UseTreeMenu = UseTreeMenu;

			try {
				var content = new FormUrlEncodedContent (new[] {
					new KeyValuePair<string, string> (Settings.WP_USERNAME_FIELD, Username),
					new KeyValuePair<string, string> (Settings.WP_PWD_FIELD, Password)
				});

				NativeCookieHandler cookieHandler = Settings.mNativeCookieHandler;//new NativeCookieHandler ();
				NativeMessageHandler handler = new NativeMessageHandler(true,true,cookieHandler){
					UseCookies = true,
				};

				handler.AllowAutoRedirect = false;
				HttpClient client = new HttpClient (handler);

				HttpResponseMessage response =  await client.PostAsync (Settings.LoginUrl, content);

				if (response.StatusCode == HttpStatusCode.Redirect) {
					Debug.WriteLine("Redirect: " + response.ToString());
					Settings.wpLoggedIn = true;
					Settings.wpUsername = Username;
					Settings.wpPassword = Password;
				} else {
					string resultContent = await response.Content.ReadAsStringAsync ();

					response.EnsureSuccessStatusCode ();
					string responseUri = response.RequestMessage.RequestUri.ToString ();
					Debug.WriteLine (responseUri);
					Debug.WriteLine(resultContent);
					if (Settings.LoginUrl.ToLower ().Equals (responseUri.ToLower ())) {
						//login failed
						Error = Settings.MSG_INVALID_USERNAME_OR_PWD;
					} else {
						Settings.wpLoggedIn = true;
						Settings.wpUsername = Username;
						Settings.wpPassword = Password;
					}
				}

				if (Settings.wpLoggedIn) {
					if (Settings.CHECK_UPDATE) {
						var buildDetail = Mvx.Resolve<IBuildDetails>();
						var respUpdate = await Service.GetUpdate(new RequestUpdate(buildDetail.OS, buildDetail.VersionCode));
						if (respUpdate.Update_info != null) {
							Settings.UpdateInfo = respUpdate;
							IsLoading = false;
							return;
						}

						System.Diagnostics.Debug.WriteLine("Build detail os={0} version_code={1}", buildDetail.OS, buildDetail.VersionCode);
					}
					//go to profile to get more cookies
					response = await client.GetAsync(Settings.ProfileUrl);

					//get user info and gen cookie for json api auth controller
					var retAuth = await Service.GenrateAuthCookie();

					if (retAuth != null) {
						Settings.WP_AuthCookie = retAuth;
					} else {
						Settings.WP_AuthCookie = null;
					}
				}
			} catch (Exception e ){
				Error = Settings.MSG_NETWORK_COMMON;
				#if DEBUG
				System.Diagnostics.Debug.WriteLine("Login Error: " + e.Message);
				Error += e.ToString();
				#endif
			}				


			Info = string.Empty;
			IsLoading = false;
		}

		async private void LoadHomePosts() {
			

//			//load home posts
			var _GroupedPosts = new ObservableCollection<CatalogPostsGroup> ();
			var cpbk = new CatalogPostsGroup ();
			//cpbk.Category = new Category(-1, "Breaking Category");

			_GroupedPosts.Add(cpbk);

			var homePosts =await Service.GetHomePosts (new RequestHomePosts ());
			foreach (HomePostGroup gr in homePosts.Home_posts) {
				if (gr.Category.Breaking_news == 1) {
					var post = gr.Posts.ToArray()[0];
					var bk = new BreakingNews(post);
					cpbk.Add(bk);
				} else {
					var cp = new CatalogPostsGroup ();
					cp.Title = gr.Category.Title;
					cp.ShortTitle = cp.Title;
					cp.Category = gr.Category;
					//ListPost.Clear ();
					var pas = gr.Posts.ToArray ();
					for (int i = 0; i < pas.Length; i++) {
						cp.Add(pas[i]);
					}

					_GroupedPosts.Add (cp);
				}
			}
//
//			foreach (Category cat in cats.Categories)
//				if ((cat.Visible == 1 && cat.Parent == 0) || cat.Id == Settings.WP_SLIDEMAIN_ID)
//				{
//					var req = new RequestCategoryPosts ();
//					req.Id = cat.Id;
//					req.Page = 1;
//					req.Count = 3;
//
//					ResponseListPost resPosts = null;
//					try{
//						if (req.GetType () == typeof(RequestRecentPosts)) {
//							resPosts = await Service.GetRecentPosts ((RequestRecentPosts)req);
//						} else {
//							resPosts = await Service.GetCategoryPosts ((RequestCategoryPosts)req);
//						}
//
//						if (cat.Id == Settings.WP_SLIDEMAIN_ID) {
//							//News at index=0 of slide main catalog is breaking news
//							var pas = resPosts.Posts.ToArray ();
//							var bk = new BreakingNews(pas[0]);
//							cpbk.Add(bk);
//						} else {
//							var cp = new CatalogPostsGroup ();
//							cp.Title = cat.Title;
//							cp.ShortTitle = cp.Title;
//							cp.Category = cat;
//							//ListPost.Clear ();
//							var pas = resPosts.Posts.ToArray ();
//							for (int i = 0; i < pas.Length; i++) {
//								cp.Add(pas[i]);
//							}
//
//							_GroupedPosts.Add (cp);
//						}
//
//						System.Diagnostics.Debug.WriteLine("HomeViewModel loaded cat posts: " + cat.Title);
//					} catch (Exception e){
//
//					}
//
//				}

			Settings.GroupedPosts = _GroupedPosts;
		}
    }

	public class UpdateClientEventArgs : EventArgs
	{
		public string Title { get; private set; }
		public string Message { get; private set; }
		public string CloseTitle { get; private set;}
		public UpdateClientEventArgs(string title, string message, string closeTitle)
		{
			Title = title;
			Message = message;
			CloseTitle = closeTitle;
		}
	}
}
