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
using FSoft.WordApp.Core;

namespace FSoft.WordApp.IOS.Views
{
	public sealed partial class MenuView : BaseViewController
	{
		private MenuViewModel MenuViewModel { get { return base.ViewModel as MenuViewModel;} }

		public MenuView () : base ("MenuView", null)
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

			//this.setRoundedView (viewUserCaption, (float)viewUserCaption.Frame.Width);


			var TableSourceMenu = new TableSourceMenu (tableViewCategories);
			this.CreateBinding (TableSourceMenu).To<MenuViewModel> (vm => vm.OptionItems).Apply ();
			this.CreateBinding (TableSourceMenu).For (s => s.SelectionChangedCommand).To<MenuViewModel> (vm => vm.CatalogSelectedCommand).Apply ();
			tableViewCategories.Source = TableSourceMenu;
			tableViewCategories.ReloadData ();

			//tune UI programatically
			Relayout();
		}

		private void Relayout() {
			AppDelegate app = UIApplication.SharedApplication.Delegate as AppDelegate;

		}
	}
}

