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
using Cirrious.MvvmCross.Touch.Views;
using FSoft.WordApp.Core.ViewModels;
using FSoft.WordApp.Core.Models;
using Cirrious.MvvmCross.Binding.BindingContext;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.CrossCore.Converters;
using Foundation;
using iOSUILib;
using SidebarNavigation;
using Cirrious.MvvmCross.ViewModels;
using FSoft.WordApp.Core;
using FSoft.WordApp.Core.Models;

namespace FSoft.WordApp.iOS.Views
{
	public sealed partial class RootView : MvxViewController
	{
		private RootViewModel RootViewModel {get { return base.ViewModel as RootViewModel;}}

		public RootView () : base ("RootView", null)
		{
			//ViewDidLoad();
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

			if (RootViewModel == null)
				return;

			AppDelegate app = UIApplication.SharedApplication.Delegate as AppDelegate;

			// create a slideout navigation controller with the top navigation controller and the menu view controller
			app.SidebarController = new SidebarController(this, CreateViewFor(RootViewModel.Home, false), CreateViewFor(RootViewModel.Menu, true));
			app.SidebarController.ReopenOnRotate = false;
			app.SidebarController.MenuLocation = SidebarController.MenuLocations.Left;
			app.SidebarController.HasShadowing = true;

			View.SetHeight (Settings.DeviceInfo.ScreenHeight);
			app.SidebarController.View.SetHeight (Settings.DeviceInfo.ScreenHeight);

			RootViewModel.Menu.MenuSelected += (sender, e) => {
				app.SidebarController.ToggleMenu();

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
			RootViewModel.Menu.SignoutSelected += (sender, e) => {
				//this.Navigation.PopToRootAsync();
				RootViewModel.SignOut();
			};
			RootViewModel.RefreshData ();
		}

		// from Stuart Lodge N+1-25
		private UIViewController CreateViewFor(IMvxViewModel viewModel, bool navBarHidden)
		{
			var controller = new UINavigationController();
			var screen = this.CreateViewControllerFor(viewModel) as UIViewController;
			controller.PushViewController(screen, false);
			controller.NavigationBarHidden = navBarHidden;
			return controller;
		}
	}
}

