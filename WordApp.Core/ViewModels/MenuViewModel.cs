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
using Cirrious.MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using FSoft.WordApp.Core.Models;
using System;
using Cirrious.CrossCore;
using FSoft.WordApp.Core.Services;

namespace FSoft.WordApp.Core.ViewModels
{
	public class MenuViewModel : MvxViewModel
	{
		public ObservableCollection<OptionItem> OptionItems { get; set; }
		public event EventHandler MenuSelected;
		public event EventHandler SignoutSelected;

		private WPUser _User;
		public WPUser User {
			get {
				return _User;
			}
			set {
				_User = value;
				if (_User != null) {
					
				}
			}
		}

		private bool _LoggedIn;
		public bool LoggedIn {
			get { return _LoggedIn;}
			set { SetProperty (ref _LoggedIn, value, "LoggedIn");}
		}

		public MenuViewModel() {
			OptionItems = new ObservableCollection<OptionItem> ();


		}

		async public void RefreshData(IFNewsService Service){
			LoggedIn = Settings.wpLoggedIn;

			if (LoggedIn) {
				if (Settings.WP_AuthCookie != null && Settings.WP_AuthCookie.User != null) {
					Settings.WP_AuthCookie.User.Capitalize = Settings.WP_AuthCookie.User.Displayname.ToUpper () [0] + "";
					User = Settings.WP_AuthCookie.User;
				} else {

				}
			}

			if (Settings.Categories == null) {
				var cats = await Service.GetListCategory (new RequestListCategory ());
				Settings.Categories = cats.Categories;
			}

			OptionItems.Clear();
			if (Settings.Categories.Count == 0) {
				var cats = await Service.GetListCategory (new RequestListCategory ());
				Settings.Categories = cats.Categories;
			}

			AddCategories (true);
		}

		public void AddCategories(bool clean) {
			var categories = Settings.Categories;
			var _OptionItems = new ObservableCollection<OptionItem> ();
			_OptionItems.Add (new CategoryOptionItem (new Category(Settings.RECENT_POST_CATEGORY_ID, Settings.RECENT_POST_CATEGORY_TITLE)));
			foreach (Category category in categories) {
				if (category.Visible == 1)
				if (category.Parent == 0) {
					_OptionItems.Add (new CategoryOptionItem (category));
					if (Settings.UseTreeMenu) {
						foreach (Category subCategory in categories) {
							if (subCategory.Parent == category.Id) {
								_OptionItems.Add (new CategoryOptionItem (subCategory));
							}
						}
					}
				}
			}

			if (Settings.wpLoggedIn) {
				_OptionItems.Insert (0, User);
				_OptionItems.Add (new SignoutOptionItem ());
			} else {
				_OptionItems.Insert (0, new AppInfoOptionItem ());
			}

			OptionItems.Clear ();
			OptionItems = _OptionItems;
			RaisePropertyChanged (() => OptionItems);
		}

		public void Add(OptionItem item) {
			OptionItems.Add (item);
		}


		private Cirrious.MvvmCross.ViewModels.MvxCommand<OptionItem> _catalogSelectedCommand;
		public System.Windows.Input.ICommand CatalogSelectedCommand
		{
			get
			{
				_catalogSelectedCommand = _catalogSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand<OptionItem>(DoSelectCatalog);
				return _catalogSelectedCommand;
			}
		}

		private void DoSelectCatalog(OptionItem item)
		{
			if (MenuSelected != null)
				MenuSelected (this, new MenuEventArgs (item));
			
			if (item.Type == OptionItemType.Category) {
				if (Settings.USE_RECENT_POSTS) {
					//ShowViewModel<RecentPostsViewModel>(new {CategoryId = item.Id, CategoryName=item.Title});
				} else {
					ShowViewModel<CatalogNewsViewModel> (new {CategoryId = item.Id, CategoryName = item.Title});
				}
			} else if (item.Type == OptionItemType.AppInfo) {
				DoShowLoginView ();
			} else if (item.Type == OptionItemType.Signout){
				SignoutCommand.Execute (null);
			}

		}

		private Cirrious.MvvmCross.ViewModels.MvxCommand _ShowLoginViewCommand;
		public System.Windows.Input.ICommand ShowLoginViewCommand
		{
			get
			{
				_ShowLoginViewCommand = _ShowLoginViewCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoShowLoginView);
				return _ShowLoginViewCommand;
			}
		}

		async private void DoShowLoginView()
		{

			Settings.wpLoggedIn = false;

			Close(this);
			ShowViewModel<LoginViewModel> ();
		}

		private Cirrious.MvvmCross.ViewModels.MvxCommand _signoutCommand;
		public System.Windows.Input.ICommand SignoutCommand
		{
			get
			{
				_signoutCommand = _signoutCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoSignout);
				return _signoutCommand;
			}
		}

		async private void DoSignout()
		{
			System.Diagnostics.Debug.WriteLine ("Signout");
			//TODO: impl real signout 
			//Close(this);
			//ShowViewModel<LoginViewModel> ();

			Settings.wpLoggedIn = false;

			if (SignoutSelected != null) {
				SignoutSelected (this, EventArgs.Empty);
			}
//			Close(this);
			var buildDetail = Mvx.Resolve<IBuildDetails>();
			if (buildDetail != null && IBuildDetails.OS_ANDROID.Equals(buildDetail.OS)){
				ShowViewModel<LoginViewModel> ();
			}
		}
	}

	public class MenuEventArgs : EventArgs
	{
		public object Object { get; private set; }

		public MenuEventArgs(object o)
		{
			Object = o;
		}
	}
}

