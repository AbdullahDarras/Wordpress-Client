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
using Android.OS;
using Cirrious.MvvmCross.Droid.FullFragging.Fragments;
using Android.Views;
using System.Collections.ObjectModel;
using FSoft.WordApp.Core.Models;
using FSoft.WordApp.Core.ViewModels;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;
using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Android.Content;
using System.Collections;
using Android.Util;

namespace FSoft.WordApp.Droid
{
	public class MenuView : MvxFragment
	{
		private IMvxDeviceInfo _deviceInfoPlugin;
		private DeviceInfo _deviceInfo;
		private RootView _rootView;
		public static MenuView NewInstance()
		{
			var frag = new MenuView { Arguments = new Bundle() };

			return frag;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);
			this.EnsureBindingContextIsSet(savedInstanceState);
			View _view = this.BindingInflate(Resource.Layout.MenuView, null);

			_deviceInfoPlugin = Mvx.Resolve<IMvxDeviceInfo>();
			_deviceInfo = _deviceInfoPlugin.GetDeviceInfo ();

			View mv = _view.FindViewById (Resource.Id.menu_view_layout_main);
			TreeListView treeView = (TreeListView)_view.FindViewById (Resource.Id.menu_categories_listview);
			return _view;
		}

		public class TreeListView : MvxListView
		{
			public TreeListView(Context context, IAttributeSet attrs) :
			base(context, attrs, new TreeListViewAdapter(context))
			{
			}
		}

		public class TreeListViewAdapter
			: MvxAdapter
		{
			private ObservableCollection<OptionItem> mData;

			public TreeListViewAdapter(Context context, IMvxAndroidBindingContext BindingContext)
				: base(context, BindingContext)
			{

			}
			public TreeListViewAdapter(Context context)
				: base(context, MvxAndroidBindingContextHelpers.Current())
			{

			}

			protected override void SetItemsSource(IEnumerable list)
			{
				mData = list as ObservableCollection<OptionItem>;

				base.SetItemsSource(mData);
			}


			public override int Count {
				get { 
					if (mData != null) return mData.Count; 
					return 0;
				}
			}

			public override long GetItemId (int position) {
				if (mData != null)
				return mData [position].Id;
				return -1;
			}

			public override int GetItemViewType(int position)
			{
				var item = GetRawItem(position) as OptionItem;
				if (item is CategoryOptionItem) {
					if (((CategoryOptionItem)item).Category.Parent == 0)
						return 0;
					return 1;
				} else if (item is SignoutOptionItem) {
					return 2;
				} else if (item is AppInfoOptionItem) {
					return 3;
				} else if (item is WPUser) {
					return 4;
				}

				
				return 0;
			}

			public override int ViewTypeCount
			{
				get { return 5; }
			}

			public override View GetView (int position, View convertView, ViewGroup parent)
			{
				return base.GetView (position, convertView, parent);
			}

			protected override global::Android.Views.View GetBindableView(global::Android.Views.View convertView, object dataContext, int templateId)
			{
				if (dataContext is CategoryOptionItem) {
					var cat = dataContext as CategoryOptionItem;
					if (cat.Category.Parent == 0) {
						return base.GetBindableView (convertView, dataContext, Resource.Layout.menu_catalog_item);
					} else {
						return base.GetBindableView (convertView, dataContext, Resource.Layout.menu_subcatalog_item);
					}
				} else if (dataContext is SignoutOptionItem) {
					return base.GetBindableView (convertView, dataContext, Resource.Layout.menu_signout_button);
				} else if (dataContext is AppInfoOptionItem) {
					return base.GetBindableView (convertView, dataContext, Resource.Layout.menu_app_info_item);
				} else if (dataContext is WPUser) {
					return base.GetBindableView (convertView, dataContext, Resource.Layout.menu_user_info);
				}

				return null;
			}
		}
	}
}

