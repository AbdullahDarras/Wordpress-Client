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

﻿using System;
using FSoft.WordApp.Core.Models;
using System.Collections.ObjectModel;
using Cirrious.MvvmCross.ViewModels;
using FSoft.WordApp.Core.Services;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;

namespace FSoft.WordApp.Core.ViewModels
{
	public class CatalogNewsViewModel : BaseViewModel
	{
		public CatalogNewsViewModel(IFNewsService service, IMvxDeviceInfo deviceInfoPlugin): base(service, deviceInfoPlugin) {
			ListPost = new ObservableCollection<Post> ();
		}

		public ObservableCollection<Post>  ListPost { get; set;}


		private bool hasMorePage;
		public bool HasMorePage {
			get { return hasMorePage; }
			set { SetProperty (ref hasMorePage, value, "HasMorePage"); }
		}

		private string _title;
		public string Title {
			get { return _title;}
			set { SetProperty (ref _title, value, "Title"); }
		}

		private Cirrious.MvvmCross.ViewModels.MvxCommand<Post> _catalogNewsSelectedCommand;
		public System.Windows.Input.ICommand CatalogNewsSelected
		{
			get
			{
				_catalogNewsSelectedCommand = _catalogNewsSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand<Post>(DoSelectCatalogNews);
				return _catalogNewsSelectedCommand;
			}
		}

		private Cirrious.MvvmCross.ViewModels.MvxCommand _moreNewsSelectedCommand;
		public System.Windows.Input.ICommand MoreNewsCommand
		{
			get
			{
				_moreNewsSelectedCommand = _moreNewsSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoLoadMoreNews);
				return _moreNewsSelectedCommand;
			}
		}

		private Cirrious.MvvmCross.ViewModels.MvxCommand _BackCommand;
		public System.Windows.Input.ICommand BackCommand
		{
			get
			{
				_BackCommand = _BackCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>Close(this));
				return _BackCommand;
			}
		}

		int mCategoryId;
		int mCurrentPage;
		int mPages;

		public virtual void Init(int CategoryId, string CategoryName){
			mCategoryId = CategoryId;
			mCurrentPage = 1;
			mPages = 1;

			Title = CategoryName;
		}

		public override void Start() {
			base.Start ();

			//LoadData (mCategoryId, mCurrentPage);
		}

		private void DoSelectCatalogNews(Post item)
		{
			ShowViewModel<PostViewModel>(item);
		}

		private void DoLoadMoreNews()
		{
			if (IsLoading)
				return;
			if (mCurrentPage < mPages) {
				mCurrentPage++;
				LoadData (mCategoryId, mCurrentPage);
			} else {
				HasMorePage = false;
			}
		}

		public void ClearData() {
			ListPost.Clear ();
			LastTimeLoadedData = -1;
		}

		public override void RefreshData ()
		{
			if (LastTimeLoadedData == -1 || ListPost.Count == 0 || DateTime.Now.Millisecond - LastTimeLoadedData > Settings.HOME_REFRESH_TIME) {
				ListPost.Clear ();
				LoadData (mCategoryId, 1);
				LastTimeLoadedData = DateTime.Now.Millisecond;
			}
		}

		async public void LoadData(int id, int page = 1) {
			if (Status == NetworkStatus.NotReachable) {//true || 
				ShowErrorMessage (Settings.MSG_NETWORK_NOT_REACHABLE);
				return;
			}

			RequestBase req = null;
			if (Settings.USE_RECENT_POSTS && id == Settings.RECENT_POST_CATEGORY_ID) {
				req = new RequestRecentPosts ();
				((RequestRecentPosts)req).Page = page;
			} else {
				req = new RequestCategoryPosts ();
				((RequestCategoryPosts)req).Id = id;
				((RequestCategoryPosts)req).Page = page;
			}


			IsLoading = true;

			ResponseListPost resPosts = null;
			try{
				if (req.GetType () == typeof(RequestRecentPosts)) {
					resPosts = await Service.GetRecentPosts ((RequestRecentPosts)req);
				} else {
					resPosts = await Service.GetCategoryPosts ((RequestCategoryPosts)req);
				}

				mPages = resPosts.Pages;

				//ListPost.Clear ();
				var pas = resPosts.Posts.ToArray ();
				for (int i = 0; i < pas.Length; i++) {
					Add (pas [i], true); //check exist when update list
					//ListPost.Add(pas [i]);
				}

				HasMorePage = (mCurrentPage < mPages);
				RaisePropertyChanged("ListPost");
			} catch (Exception e){
				ShowErrorMessage (Settings.MSG_NETWORK_COMMON, e);
			}


			IsLoading = false;
		}

		public void Add(Post post, bool checkExist = false) {
			if (checkExist) {
				if (isExistPost(post.Id)) return;
			}

			ListPost.Add(post);
			//System.Diagnostics.Debug.WriteLine ("Added Post: " + post.Title);
		}

		private bool isExistPost(int id){
			foreach (Post p in ListPost) {
				if (p.Id == id) {
					return true;
				}
			}

			return false;
		}
	}
}

