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

﻿
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using FSoft.WordApp.Core.ViewModels;
using FSoft.WordApp.Core;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;

namespace FSoft.WordApp.iOS.Views
{
	public partial class LoginView : BaseViewController
	{
		private LoginViewModel LoginViewModel { get {return base.ViewModel as LoginViewModel;}}

		private bool isLoading;
		public bool IsLoading {
			get {return isLoading; }
			set { 
				isLoading = value;
				if (value)
					View.EndEditing (true);//hide keyboard
			}
		}


		private bool isRememberPwd;
		public bool RememberPwd {
			get {return isRememberPwd; }
			set { 
				isRememberPwd = value;
				swRememberPassword.On = value;
			}
		}

		public LoginView () : base ("LoginView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RegisterForKeyboardNotifications ();

			LoginViewModel.UpdateClientHandler += (sender, e) => {
				var ee = (UpdateClientEventArgs)e;
				var dispatcher = Mvx.Resolve<IMvxMainThreadDispatcher> ();
				dispatcher.RequestMainThreadAction (() => {
					UIAlertView alert = new UIAlertView (ee.Title, ee.Message, null, ee.CloseTitle, null);
					alert.Clicked += (sender2, e2) => {
						LoginViewModel.showUpdateStore();
						System.Environment.Exit(0);
					};
					alert.Show ();
				});
			};

			// Perform any additional setup after loading the view, typically from a nib.
			this.CreateBinding (txtUsername).For("Text").To<LoginViewModel> (vm => vm.Username).Apply ();
			this.CreateBinding (txtPassword).For("Text").To<LoginViewModel> (vm => vm.Password).Apply ();

			this.CreateBinding (btnLogin).For ("TouchUpInside").To<LoginViewModel> (vm => vm.LoginCommand).Apply ();

			this.CreateBinding (btnLogin).For ("Hidden").To<LoginViewModel> (vm => vm.IsNotBusy).WithConversion ("Visibility").Apply ();
			this.CreateBinding (viewInfo).For ("Hidden").To<LoginViewModel> (vm => vm.IsLoading).WithConversion ("Visibility").Apply ();

			//this.CreateBinding (swRememberPassword).To<LoginViewModel> (vm => vm.RememberPassword).Apply ();

			this.CreateBindingSet<LoginView, LoginViewModel> ().Bind (this).For (v => v.IsLoading).To (vm => vm.IsLoading).Apply ();
//			this.CreateBindingSet<LoginView, LoginViewModel> ().Bind (this).For (v => v.RememberPwd).To (vm => vm.RememberPassword).Apply ();

			swRememberPassword.On = LoginViewModel.RememberPassword;
			swRememberPassword.ValueChanged += (sender, e) => {
				var sw = (iOSUILib.MDSwitch)sender;
				LoginViewModel.RememberPassword = sw.On;
			};
			DismissKeyboardOnBackgroundTap ();

			txtUsername.EditingDidBegin += (object sender, EventArgs e) => {
				viewTxtUsernameDivider.BackgroundColor = COLOR_HIGHLIGHT;
			};
			txtUsername.EditingDidEnd += (object sender, EventArgs e) => {
				viewTxtUsernameDivider.BackgroundColor = UIColor.LightGray;
			};

			txtPassword.EditingDidBegin += (object sender, EventArgs e) => {
				viewTxtPwdDivider.BackgroundColor = COLOR_HIGHLIGHT;
			};
			txtPassword.EditingDidEnd += (object sender, EventArgs e) => {
				viewTxtPwdDivider.BackgroundColor = UIColor.LightGray;
			};

			Relayout ();
		}

		private void Relayout() {
			imgLogo.Frame = new CoreGraphics.CGRect (0,0,Settings.DeviceInfo.ScreenWidth,imgLogo.Frame.Height);
			//lbCopyright.Frame = new CoreGraphics.CGRect (0,lbCopyright.Frame.Top,Settings.DeviceInfo.ScreenWidth,lbCopyright.Frame.Height);

			txtUsername.Frame = new CoreGraphics.CGRect (8,txtUsername.Frame.Top,Settings.DeviceInfo.ScreenWidth - 16,txtUsername.Frame.Height);
			txtPassword.Frame = new CoreGraphics.CGRect (8,txtPassword.Frame.Top,Settings.DeviceInfo.ScreenWidth - 16,txtPassword.Frame.Height);

			viewInput.SetWidth (Settings.DeviceInfo.ScreenWidth);
			viewTxtUsernameDivider.Frame = new CoreGraphics.CGRect (8,viewTxtUsernameDivider.Frame.Top,Settings.DeviceInfo.ScreenWidth - 16,viewTxtUsernameDivider.Frame.Height);
			viewTxtPwdDivider.Frame = new CoreGraphics.CGRect (8,viewTxtPwdDivider.Frame.Top,Settings.DeviceInfo.ScreenWidth - 16,viewTxtPwdDivider.Frame.Height);

			swRememberPassword.Frame = new CoreGraphics.CGRect (Settings.DeviceInfo.ScreenWidth - swRememberPassword.Frame.Width - 8, swRememberPassword.Frame.Top, swRememberPassword.Frame.Width,swRememberPassword.Frame.Height);

			btnLogin.Frame = new CoreGraphics.CGRect (0,btnLogin.Frame.Top,Settings.DeviceInfo.ScreenWidth,btnLogin.Frame.Height);
			viewInfo.Frame = new CoreGraphics.CGRect (0,viewInfo.Frame.Top,Settings.DeviceInfo.ScreenWidth,viewInfo.Frame.Height);

			lbCopyright.SetBottom (Settings.DeviceInfo.ScreenHeight);
		}

		protected override  void OnKeyboardChanged (bool visible, float keyboardHeight)
		{
			//View.LayoutSubviews ();
			base.OnKeyboardChanged(visible, keyboardHeight);
			var buildDetail = Mvx.Resolve<IBuildDetails>();
			if (buildDetail != null && buildDetail.DeviceType == IBuildDetails.DeviceTypeDef.IPAD)
				return; //dont need relayout
			if (visible) {
				System.Diagnostics.Debug.WriteLine ("loginview keyboard showed");
				//lbCopyright.Hidden = true;
				viewInput.Frame = new CoreGraphics.CGRect(0,150, viewInput.Frame.Width, viewInput.Frame.Height);//viewInput.Frame.Top - keyboardHeight
			} else {
				//lbCopyright.Hidden = false;
				System.Diagnostics.Debug.WriteLine ("loginview keyboard hided");
				viewInput.Frame = new CoreGraphics.CGRect(0,220,viewInput.Frame.Width, viewInput.Frame.Height);
			}
		}
	}
}

