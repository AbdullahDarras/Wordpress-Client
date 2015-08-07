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
using Cirrious.MvvmCross.Binding.BindingContext;
using FSoft.WordApp.Core.ViewModels;
using FSoft.WordApp.Core;

namespace FSoft.WordApp.iOS.Views
{
	public partial class RecentPostsView : CatalogNewsView
	{
		private string _title;
		public string CatTitle {
			get { return _title;}
			set { 
				_title = value;
				if (Settings.RECENT_POST_CATEGORY_TITLE.Equals (_title)) {
					_title = "TechInsight";
				}
				this.NavigationItem.TitleView = CreateNavTitle (_title);
			}
		}

		public RecentPostsView () : base ()
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
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.NavigationItem.TitleView = CreateNavTitle ("TechInsight");//
			//			this.NavigationItem.TitleView.ContentMode = UIViewContentMode.Left;
			//this.NavigationItem.TitleView.BackgroundColor = UIColor.Red;

			AppDelegate app = UIApplication.SharedApplication.Delegate as AppDelegate;


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("menu")
					, UIBarButtonItemStyle.Done
					, (sender, args) => app.SidebarController.ToggleMenu ()), true);

			//this.CreateBinding (prgLoading).For ("Hidden").To<HomeViewModel> (vm => vm.IsLoading).WithConversion ("Visibility").Apply ();
			this.CreateBindingSet<RecentPostsView, RecentPostsViewModel > ().Bind(this).For(v=>v.CatTitle).To(item=>item.Title).Apply();

//			this.NavigationItem.SetRightBarButtonItem(
//				new UIBarButtonItem(UIImage.FromBundle("refresh")
//					, UIBarButtonItemStyle.Plain
//					, (sender, args) => {
//						CatalogNewsViewModel.com.Execute(null);
//					}), true);
		}
	}
}

