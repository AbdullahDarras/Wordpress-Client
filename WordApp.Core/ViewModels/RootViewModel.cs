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
using Cirrious.MvvmCross.ViewModels;
using FSoft.WordApp.Core.Services;
using FSoft.WordApp.Core.Models;
using Cirrious.CrossCore;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;

namespace FSoft.WordApp.Core.ViewModels
{
	public class RootViewModel  : BaseViewModel
	{
		IFNewsService Service;
		public RootViewModel(IFNewsService service, IMvxDeviceInfo deviceInfoPlugin):base(service,deviceInfoPlugin)
		{
			if (Settings.USE_RECENT_POSTS ){
				Home = new RecentPostsViewModel(service, deviceInfoPlugin);
				((RecentPostsViewModel)Home).Init(Settings.RECENT_POST_CATEGORY_ID, "Home");
			} else {
				Home = new HomeViewModel(service, deviceInfoPlugin);
			}
			Menu = new MenuViewModel();
			Service = service;

			Mvx.Trace ("Init RootViewModel");
		}
		private BaseViewModel _home;
		public BaseViewModel Home
		{
			get { return _home; }
			set { _home = value; RaisePropertyChanged(() => Home); }
		}

		private MenuViewModel _menu;
		public MenuViewModel Menu
		{
			get { return _menu; }
			set { _menu = value; RaisePropertyChanged(() => Menu); }
		}

		public override void Start()
		{
			base.Start();

			//LoadData();
		}

		public void SignOut() {
			Close (this);
			var buildDetail = Mvx.Resolve<IBuildDetails>();
			if (IBuildDetails.OS_ANDROID.Equals(buildDetail.OS)) {
				ShowViewModel<LoginViewModel> ();
			}
		}

		async public override void RefreshData() {
			if (Settings.Categories == null && Settings.WP_NEED_LOGIN == false) {
				var cats = await Service.GetListCategory (new RequestListCategory ());
				Settings.Categories = cats.Categories;
			}

			Menu.OptionItems.Clear();
			if (Settings.Categories.Count == 0) {
				var cats = await Service.GetListCategory (new RequestListCategory ());
				Settings.Categories = cats.Categories;
			}

			Menu.AddCategories (true);

			Home.RefreshData ();
		}
	}
}

