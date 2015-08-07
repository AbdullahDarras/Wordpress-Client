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

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using FSoft.WordApp.Core.Models;
using FSoft.WordApp.Core.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;

namespace FSoft.WordApp.Core.ViewModels
{
	public class HomeViewModel : BaseViewModel
    {

		public ObservableCollection<CatalogPostsGroup> GroupedPosts { get; set; }
		public ObservableCollection<object> FlatListPost { get; set; }
		public ObservableCollection<Post> ListPost { get; set; }

		public HomeViewModel(IFNewsService service, IMvxDeviceInfo deviceInfoPlugin) : base(service, deviceInfoPlugin)
		{
			GroupedPosts = new ObservableCollection<CatalogPostsGroup> ();
		}

		public override void RefreshData() {
			if (LastTimeLoadedData == -1 || GroupedPosts.Count == 0 || DateTime.Now.Millisecond - LastTimeLoadedData > Settings.HOME_REFRESH_TIME) {
				LoadData ();
				LastTimeLoadedData = DateTime.Now.Millisecond;
			}
		}

		async public void LoadData() {
			if (Status == NetworkStatus.NotReachable) {//true || 
				ShowErrorMessage (Settings.MSG_NETWORK_NOT_REACHABLE);
				return;
			}

			IsLoading = true;

			try {
				//load home posts
				var _GroupedPosts = new ObservableCollection<CatalogPostsGroup> ();
				var _flatPosts = new ObservableCollection<object>();
				var cpbk = new CatalogPostsGroup ();
				//cpbk.Category = new Category(-1, "Breaking Category");

				_GroupedPosts.Add(cpbk);

				var homePosts =await Service.GetHomePosts (new RequestHomePosts ());
				foreach (HomePostGroup gr in homePosts.Home_posts) {
					if (gr.Category.Breaking_news == 1) {
						var post = gr.Posts.ToArray()[0];
						var bk = new BreakingNews(post);
						cpbk.Add(bk);
						_flatPosts.Insert(0,bk);
					} else {
						var cp = new CatalogPostsGroup ();
						cp.Title = gr.Category.Title;
						cp.ShortTitle = cp.Title;
						cp.Category = gr.Category;
						_flatPosts.Add(gr.Category);
						//ListPost.Clear ();
						var pas = gr.Posts.ToArray ();
						for (int i = 0; i < pas.Length; i++) {
							cp.Add(pas[i]);
							_flatPosts.Add(pas[i]);
						}

						_GroupedPosts.Add (cp);
					}
				}

				GroupedPosts.Clear();
				GroupedPosts = _GroupedPosts;
				RaisePropertyChanged("GroupedPosts");

				FlatListPost = _flatPosts;
				RaisePropertyChanged("FlatListPost");
			} catch (Exception e){
				System.Diagnostics.Debug.WriteLine ("home " + e);
				ShowErrorMessage (Settings.MSG_NETWORK_COMMON, e);
			}

			IsLoading = false;
		}

		private void initGroupedList() {
			for (int i = 0; i < 5; i++) {
				var cp = new CatalogPostsGroup ();
				cp.Title = "Grouped Title " + i;
				cp.ShortTitle = cp.Title;
			

				for (int j = 0; j < 3; j++) {
					var p = new Post ();
					p.Title = "Post title " + j;
					p.Title_plain = "Post title " + j;
					cp.Add (p);
				}

				GroupedPosts.Add (cp);
			}
			
		}

		private Cirrious.MvvmCross.ViewModels.MvxCommand<Object> _postSelectedCommand;
		public System.Windows.Input.ICommand PostSelectedCommand
		{
			get
			{
				_postSelectedCommand = _postSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand<Object>(DoSelectPost);
				return _postSelectedCommand;
			}
		}
		private void DoSelectPost(Object item)
		{
			if (item is Post) {
				ShowViewModel<PostViewModel>(item as Post);
			} else if (item is Category) {
				System.Diagnostics.Debug.WriteLine ("Selected Category");
				if (Settings.USE_RECENT_POSTS) {
					ShowViewModel<RecentPostsViewModel>(new {CategoryId = ((Category)item).Id, CategoryName=((Category)item).Title});
				} else {
					ShowViewModel<CatalogNewsViewModel>(new {CategoryId = ((Category)item).Id, CategoryName=((Category)item).Title});
				}
			}

		}

		public event EventHandler MenuButtonSelected; //top left menu button

		private Cirrious.MvvmCross.ViewModels.MvxCommand _MenuButtonSelectedCommand;
		public System.Windows.Input.ICommand MenuSelectedCommand
		{
			get
			{
				_MenuButtonSelectedCommand = _MenuButtonSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>{
					if (MenuButtonSelected != null){
						MenuButtonSelected(this,EventArgs.Empty);
					}
				});
				return _MenuButtonSelectedCommand;
			}
		}


		private Cirrious.MvvmCross.ViewModels.MvxCommand _RefreshSelectedCommand;
		public System.Windows.Input.ICommand RefreshSelectedCommand
		{
			get
			{
				_RefreshSelectedCommand = _RefreshSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>{
					LoadData();
				});
				return _RefreshSelectedCommand;
			}
		}
    }


	public class CatalogPostsGroup : ObservableCollection<Post> {
		public string Title {get; set;}
		public string ShortTitle {get; set;}
		public Category Category { get; set;}
	}
}
