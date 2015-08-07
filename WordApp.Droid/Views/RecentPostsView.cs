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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FSoft.WordApp.Core.ViewModels;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Droid.FullFragging.Fragments;

namespace FSoft.WordApp.Droid
{
	[Activity (Label = "Recent News", MainLauncher = false, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]			
	public class RecentPostsView : MvxFragment, Android.Widget.AbsListView.IOnScrollListener
	{
		public CatalogNewsViewModel CatalogNewsViewModel { get { return base.ViewModel as CatalogNewsViewModel;}}

		Activity ctx;
		private MvxListView mPostsListView;

		public override void OnAttach (Activity activity)
		{
			ctx = activity;
			base.OnAttach (activity);
		}

		public static RecentPostsView NewInstance()
		{
			var frag = new RecentPostsView { Arguments = new Bundle() };
			return frag;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);
			this.EnsureBindingContextIsSet(savedInstanceState);
			View _view = this.BindingInflate(Resource.Layout.RecentPostsView, null);

			mPostsListView = (MvxListView)_view.FindViewById (Resource.Id.catalog_news_listview);
			mPostsListView.SetOnScrollListener (this);

			return _view;
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

