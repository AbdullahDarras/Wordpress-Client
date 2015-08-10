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
using UIKit;
using CoreGraphics;
using Cirrious.MvvmCross.Binding.BindingContext;
using FSoft.WordApp.Core.ViewModels;
using FSoft.WordApp.Core;

namespace FSoft.WordApp.IOS.Views
{
	public sealed partial class HomeView : BaseViewController
	{
		public HomeViewModel HomeViewModel { get { return base.ViewModel as HomeViewModel;}}
		public HomeView () : base ("HomeView", null)
		{
			//ViewDidLoad ();
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
			
//			// Perform any additional setup after loading the view, typically from a nib.
//			UIImageView logo = new UIImageView(UIImage.FromBundle("logo_cucumber"));
//			logo.Frame = new CoreGraphics.CGRect (15,0,320,25);
//			logo.ContentMode = UIViewContentMode.ScaleAspectFit;//UIViewContentMode.ScaleAspectFit;


			this.NavigationItem.TitleView = CreateNavTitle ("TechInsight");//
//			this.NavigationItem.TitleView.ContentMode = UIViewContentMode.Left;
			//this.NavigationItem.TitleView.BackgroundColor = UIColor.Red;

			AppDelegate app = UIApplication.SharedApplication.Delegate as AppDelegate;


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("menu")
					, UIBarButtonItemStyle.Done
					, (sender, args) => app.SidebarController.ToggleMenu ()), true);

			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("refresh")
					, UIBarButtonItemStyle.Plain
					, (sender, args) => {
						HomeViewModel.RefreshSelectedCommand.Execute(null);
					}), true);



			if (tableHomePosts == null)
				return;
			var TableSourceMenu = new TableSourceHomePosts (tableHomePosts);
			this.CreateBinding (TableSourceMenu).To<HomeViewModel> (vm => vm.FlatListPost).Apply ();
			this.CreateBinding (TableSourceMenu).For (s => s.SelectionChangedCommand).To<HomeViewModel> (vm => vm.PostSelectedCommand).Apply ();
			tableHomePosts.Source = TableSourceMenu;
			tableHomePosts.ReloadData ();

			this.CreateBinding (prgLoading).For ("Hidden").To<HomeViewModel> (vm => vm.IsLoading).WithConversion ("Visibility").Apply ();

			//HomeViewModel.RefreshData ();
			Relayout();
		}

		private void Relayout(){
			tableHomePosts.Frame = new CoreGraphics.CGRect (0,0,Settings.DeviceInfo.ScreenWidth,Settings.DeviceInfo.ScreenHeight);
		}
	}
}

