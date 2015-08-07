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

﻿using Android.OS;
using Cirrious.MvvmCross.Droid.FullFragging.Fragments;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using Android.Views;

using FSoft.WordApp.Core.ViewModels;
using Cirrious.CrossCore.Platform;
using Android.App;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Android.Content;
using System;
using Cirrious.MvvmCross.Droid.Views;

namespace FSoft.WordApp.Droid
{
	[Activity (Label = "Catalog News", MainLauncher = false, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]

	public class CatalogNewsView : MvxActivity, Android.Widget.AbsListView.IOnScrollListener
	{
		public CatalogNewsViewModel CatalogNewsViewModel { get { return base.ViewModel as CatalogNewsViewModel;}}

		private MvxListView mPostsListView;

		public static bool FromHome = true;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);


			View _view = this.BindingInflate(Resource.Layout.catalog_news_view, null);

			SetContentView (_view);

//			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
//			SupportActionBar.SetHomeAsUpIndicator (Resource.Drawable.ic_menu_grey600_36dp);
//			SupportActionBar.SetHomeButtonEnabled(true);

//			CatalogNewsViewModel.PropertyChanged += (sender, e) => {
//				if (e.PropertyName == "Title"){
//					SupportActionBar.Title = CatalogNewsViewModel.Title;
//				}
//			};

//			var s = this.CreateBindingSet<CatalogNewsView, CatalogNewsViewModel> ();
//			s.Bind (this).For (v => v.CategoryName).To (vm => vm.Title);
//			s.Apply ();

			mPostsListView = (MvxListView)this.FindViewById (Resource.Id.catalog_news_listview);
			mPostsListView.SetOnScrollListener (this);

			CatalogNewsViewModel.ErrorHandler += (sender, e) => {
				ErrorEventArgs ee = (ErrorEventArgs)e;
				var dispatcher = Mvx.Resolve<IMvxMainThreadDispatcher> ();
				dispatcher.RequestMainThreadAction (() => {
					AlertDialog.Builder builder = new AlertDialog.Builder (this);
					builder.SetTitle (ee.Title);
					builder.SetMessage (ee.Message);
					builder.SetNegativeButton (ee.CloseTitle, (EventHandler<DialogClickEventArgs>)null);
					builder.Show ();
				});
			};
			this.StartCalled += (sender, e) => {
				if (FromHome)
					this.OverridePendingTransition (Resource.Animation.slide_right, Resource.Animation.slide_left);
				else
					this.OverridePendingTransition (Resource.Animation.slide_left_right, Resource.Animation.slide_right_right);
				CatalogNewsViewModel.RefreshData ();
			};
		}

		public override void Finish ()
		{
			base.Finish ();
			RootView.FromLogin = false;
			this.OverridePendingTransition (Resource.Animation.slide_left_right, Resource.Animation.slide_right_right);
		}

		void Android.Widget.AbsListView.IOnScrollListener.OnScroll (Android.Widget.AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
		{
			//throw new System.NotImplementedException ();
		}
		void Android.Widget.AbsListView.IOnScrollListener.OnScrollStateChanged (Android.Widget.AbsListView view, Android.Widget.ScrollState scrollState)
		{
			if (scrollState == Android.Widget.ScrollState.Idle) {
				if (mPostsListView.LastVisiblePosition >= mPostsListView.Count - 1) {
					CatalogNewsViewModel.MoreNewsCommand.Execute(null);
				}
			}
		}
	}
}

