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
using Android.App;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Collections;
using Android.Content;
using FSoft.WordApp.Core.ViewModels;
using FSoft.WordApp.Core.Models;
using System.Collections.ObjectModel;
using Android.Util;

namespace FSoft.WordApp.Droid
{
	public class HomeView : MvxFragment
	{
		Activity ctx;

		public override void OnAttach (Activity activity)
		{
			ctx = activity;
			base.OnAttach (activity);
		}

		public static HomeView NewInstance()
		{
			var frag = new HomeView { Arguments = new Bundle() };
			return frag;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);
			this.EnsureBindingContextIsSet(savedInstanceState);
			View _view = this.BindingInflate(Resource.Layout.HomeView, null);

			GroupedListView list = (GroupedListView)_view.FindViewById (Resource.Id.home_post_listview);
//			list.Adapter = new GroupedListAdapter (ctx, (IMvxAndroidBindingContext)BindingContext);

			return _view;
		}
			
		public class GroupedListView : MvxListView
		{
			public GroupedListView(Context context, IAttributeSet attrs) :
			base(context, attrs, new GroupedListAdapter(context))
			{
			}
		}

		public class GroupedListAdapter
			: MvxAdapter, ISectionIndexer
		{
			private Java.Lang.Object[] _sectionHeaders;
			private List<int> _sectionLookup;
			private List<int> _reverseSectionLookup;


			public GroupedListAdapter(Context context, IMvxAndroidBindingContext BindingContext)
				: base(context, BindingContext)
			{

			}
			public GroupedListAdapter(Context context)
				: base(context, MvxAndroidBindingContextHelpers.Current())
			{
				
			}

			protected override void SetItemsSource(IEnumerable list)
			{
				var groupedList = list as ObservableCollection<CatalogPostsGroup>;

				if (groupedList == null)
				{
					_sectionHeaders = null;
					_sectionLookup = null;
					_reverseSectionLookup = null;
					base.SetItemsSource(null);
					return;
				}

				var flattened = new List<object>();
				_sectionLookup = new List<int>();
				_reverseSectionLookup = new List<int>();
				var sectionHeaders = new List<Category>();

				var groupsSoFar = 0;
				foreach (var group in groupedList)
				{
					_sectionLookup.Add(flattened.Count);
					var groupHeader = GetGroupHeader(group);

					for (int i = 0; i <= group.Count; i++)
						_reverseSectionLookup.Add(groupsSoFar);

					if (groupHeader != null) {
						sectionHeaders.Add(groupHeader);
						flattened.Add(groupHeader);
					}

					flattened.AddRange(group);

					groupsSoFar++;
				}

				_sectionHeaders = CreateJavaStringArray(sectionHeaders);

				base.SetItemsSource(flattened);
			}

			private Category GetGroupHeader(CatalogPostsGroup group)
			{
				return group.Category;
			}

			public int GetPositionForSection(int section)
			{
				if (_sectionLookup == null)
					return 0;

				return _sectionLookup[section];
			}

			public int GetSectionForPosition(int position)
			{
				if (_reverseSectionLookup == null)
					return 0;

				return _reverseSectionLookup[position];
			}

			public Java.Lang.Object[] GetSections()
			{
				return _sectionHeaders;
			}

			private static Java.Lang.Object[] CreateJavaStringArray(List<Category> inputList)
			{
				if (inputList == null)
					return null;

				var toReturn = new Java.Lang.Object[inputList.Count];
				for (var i = 0; i < inputList.Count; i++)
				{
					toReturn [i] = new Java.Lang.String(inputList[i].Title);
				}

				return toReturn;
			}

			public override int GetItemViewType(int position)
			{
				var item = GetRawItem(position);
				if (item is BreakingNews)
					return 0;
				if (item is Post)
					return 1;
				
				return 2;
			}

			public override int ViewTypeCount
			{
				get { return 3; }
			}

			protected override global::Android.Views.View GetBindableView(global::Android.Views.View convertView, object dataContext, int templateId)
			{
				if (dataContext is BreakingNews)
					return base.GetBindableView (convertView, dataContext, Resource.Layout.home_view_breaking_news);
				else if (dataContext is Post)
					return base.GetBindableView (convertView, dataContext, Resource.Layout.home_view_post_item);
				else
					return base.GetBindableView(convertView, dataContext, Resource.Layout.home_view_grouped_title);
			}
		}
	}
}

