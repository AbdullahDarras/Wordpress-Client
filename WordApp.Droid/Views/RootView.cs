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
using Android.App;
using Android.OS;
using FSoft.WordApp.Core.ViewModels;
using Cirrious.MvvmCross.Droid.FullFragging.Fragments;
using Cirrious.MvvmCross.ViewModels;
using SlidingMenuSharp;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using System;
using Android.Content;
using FSoft.WordApp.Core;
using FSoft.WordApp.Core.Models;

namespace FSoft.WordApp.Droid
{
	[Activity (Label = "Cucumber", MainLauncher = false, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]		
	public class RootView : MvxSlidingActionBarActivity
	{
		public RootViewModel RootViewModel
		{ get { return base.ViewModel as RootViewModel; } }
		private long mLastTime = -1;
		public static bool FromLogin = true;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			// Create your application here
			SetContentView (Resource.Layout.RootView);

			if (Settings.USE_RECENT_POSTS) {
				CreateViewFor (RecentPostsView.NewInstance(), RootViewModel.Home, Resource.Id.content_frame);
			} else {
				CreateViewFor (HomeView.NewInstance(), RootViewModel.Home, Resource.Id.content_frame);
			}

			SetBehindContentView(Resource.Layout.menu_frame);

			SlidingMenu.ShadowWidthRes = Resource.Dimension.shadow_width;
			SlidingMenu.BehindOffsetRes = Resource.Dimension.slidingmenu_offset;
			SlidingMenu.ShadowDrawableRes = Resource.Drawable.shadow;
			SlidingMenu.FadeDegree = 0.25f;
			SlidingMenu.TouchModeAbove = TouchMode.Fullscreen; //TouchMode.Margin;

			CreateViewFor (MenuView.NewInstance(), RootViewModel.Menu, Resource.Id.menu_frame);

			if (RootViewModel.Home is RecentPostsViewModel) {
				((RecentPostsViewModel)RootViewModel.Home).MenuButtonSelected += (sender, e) => Toggle();
			} else {
				((HomeViewModel)RootViewModel.Home).MenuButtonSelected += (sender, e) => Toggle();//top left menu button
			}

			RootViewModel.Menu.SignoutSelected += (sender, e) => {
				//do something
				Finish();
			};//close activity
			RootViewModel.Menu.MenuSelected += (sender, e) => {
				Toggle();
				if (Settings.USE_RECENT_POSTS) {
					MenuEventArgs me = (MenuEventArgs)e;
					if (me.Object is CategoryOptionItem) {
						((RecentPostsViewModel)RootViewModel.Home).ClearData();

						CategoryOptionItem cat = (CategoryOptionItem) me.Object;
						((RecentPostsViewModel)RootViewModel.Home).Init(cat.Id, cat.Title);
						((RecentPostsViewModel)RootViewModel.Home).RefreshData();
						((RecentPostsViewModel)RootViewModel.Home).Title = cat.Title;
					}
				}
			};

			RootViewModel.Home.ErrorHandler += (sender, e) => {
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
				if (FromLogin){
					FromLogin = false;
					this.OverridePendingTransition (Resource.Animation.slide_right, Resource.Animation.slide_left);
				} else
					this.OverridePendingTransition (Resource.Animation.slide_left_right, Resource.Animation.slide_right_right);

				CatalogNewsView.FromHome = true;

				if (mLastTime == -1 || SystemClock.CurrentThreadTimeMillis() - mLastTime > Settings.HOME_REFRESH_TIME){
					System.Diagnostics.Debug.WriteLine("Refresh Home Page");
					mLastTime = SystemClock.CurrentThreadTimeMillis();
					RootViewModel.RefreshData();
				}
			};

			CatalogNewsView.FromHome = true;
		}

		public override void StartActivity (Intent intent)
		{
			base.StartActivity (intent);
			this.OverridePendingTransition(Resource.Animation.slide_right,Resource.Animation.slide_left);

		}
		public void CreateViewFor(MvxFragment fragment, IMvxViewModel viewModel, int container){
	
			fragment.ViewModel = viewModel;

			FragmentManager.BeginTransaction ()
				.Replace (container, fragment)
				.Commit ();
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				Toggle();
				return true;
			}
			return base.OnOptionsItemSelected(item);
		}
	}
}

