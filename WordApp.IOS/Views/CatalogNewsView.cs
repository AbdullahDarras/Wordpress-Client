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
using FSoft.WordApp.Core.ViewModels;
using FSoft.WordApp.Core.Models;
using Cirrious.MvvmCross.Binding.BindingContext;
using FSoft.WordApp.Core;

namespace FSoft.WordApp.IOS.Views
{
	public partial class CatalogNewsView : BaseViewController, IUITableViewDelegate
	{
		public CatalogNewsViewModel CatalogNewsViewModel { get { return base.ViewModel as CatalogNewsViewModel;}}
		public CatalogNewsView () : base ("CatalogView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.NavigationBarHidden = false;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			AppDelegate app = UIApplication.SharedApplication.Delegate as AppDelegate;

			this.NavigationController.NavigationBarHidden = false;
			//this.Title = CatalogNewsViewModel.Title;
			this.NavigationItem.TitleView = CreateNavTitle(CatalogNewsViewModel.Title);
			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("back")
					, UIBarButtonItemStyle.Plain
					, (sender, args) => {
						this.NavigationController.NavigationBarHidden = true;
						CatalogNewsViewModel.BackCommand.Execute(null);
					}), true);		
		
//			this.NavigationItem.SetRightBarButtonItem(
//				new UIBarButtonItem(UIImage.FromBundle("refresh")
//					, UIBarButtonItemStyle.Plain
//					, (sender, args) => {
////						this.NavigationController.NavigationBarHidden = true;
////						CatalogNewsViewModel.BackCommand.Execute(null);
//					}), true);		

			this.CreateBinding (prgLoading).For ("Hidden").To<CatalogNewsViewModel> (vm => vm.IsLoading).WithConversion("Visibility").Apply ();

			var TableSourceMenu = new TableSourceCategoryPosts (tableCategoryPosts, CatalogNewsViewModel);
			this.CreateBinding (TableSourceMenu).To<CatalogNewsViewModel> (vm => vm.ListPost).Apply ();
			this.CreateBinding (TableSourceMenu).For (s => s.SelectionChangedCommand).To<CatalogNewsViewModel> (vm => vm.CatalogNewsSelected).Apply ();
			tableCategoryPosts.Source = TableSourceMenu;
			tableCategoryPosts.ReloadData ();

			CatalogNewsViewModel.RefreshData ();

//			tableCategoryPosts.Scrolled +=  (sender, e) => {
//				this.Scrolled(tableCategoryPosts);
//			};

//			tableCategoryPosts.Scrolled += (sender, e) => {
//				System.Diagnostics.Debug.WriteLine(e.ToString());	
//			};

			Relayout ();
		}

		private void Relayout() {
			tableCategoryPosts.Frame = new CoreGraphics.CGRect (0,0,Settings.DeviceInfo.ScreenWidth,Settings.DeviceInfo.ScreenHeight);
			prgLoading.Frame = new CoreGraphics.CGRect (Settings.DeviceInfo.ScreenWidth/2 - prgLoading.Frame.Width/2,Settings.DeviceInfo.ScreenHeight/2-prgLoading.Frame.Height/2,prgLoading.Frame.Width,prgLoading.Frame.Height);
		}
	}
}

