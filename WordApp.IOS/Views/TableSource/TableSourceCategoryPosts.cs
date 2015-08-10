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
using Cirrious.MvvmCross.Binding.Touch.Views;
using UIKit;
using Foundation;
using FSoft.WordApp.Core.ViewModels;
using FSoft.WordApp.Core;

namespace FSoft.WordApp.IOS.Views
{
	public class TableSourceCategoryPosts : MvxTableViewSource
	{
		private CatalogNewsViewModel CatalogNewsViewModel { get; set;}
		public TableSourceCategoryPosts (UITableView tableView, CatalogNewsViewModel _CatalogNewsViewModel) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("CategoryPostViewCell", NSBundle.MainBundle), CategoryPostViewCell.Key);
//			tableView.RegisterNibForCellReuse(UINib.FromName("MenuSubCategory", NSBundle.MainBundle), MenuSubCategoryCellView.Key);

			CatalogNewsViewModel = _CatalogNewsViewModel;

			tableView.SeparatorInset = UIEdgeInsets.Zero;
		}

		//Define height of Cell follow by type data for cell view
		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var item = GetItemAt (indexPath);
			return 174;

		}


		//Define type CellView
		protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
		{
			//Default is top cell
			UIColor bgUnselected;
			UIView bgSelected=new UIView();
			bgUnselected = UIColor.White;
			bgSelected.BackgroundColor = UIColor.White;

			NSString identifier = CategoryPostViewCell.Key;

			var cell = (UITableViewCell)tableView.DequeueReusableCell(identifier, indexPath);

			UIView v = cell.ViewWithTag (120);//title
			v.Frame = new CoreGraphics.CGRect (v.Frame.Left, v.Frame.Top, Settings.DeviceInfo.ScreenWidth - 100 - Settings.PADDING_IOS*3, v.Frame.Height);
			v = cell.ViewWithTag (121);//sapo
			v.Frame = new CoreGraphics.CGRect (v.Frame.Left, v.Frame.Top, Settings.DeviceInfo.ScreenWidth - Settings.PADDING_IOS * 2, v.Frame.Height);


			cell.BackgroundColor = bgUnselected;
			cell.SelectedBackgroundView = bgSelected;

			cell.LayoutMargins = UIEdgeInsets.Zero;
			return cell;
		}

		public override void Scrolled (UIScrollView scrollView)
		{
			if(scrollView.ContentOffset.Y >= (scrollView.ContentSize.Height - scrollView.Frame.Size.Height)){
				CatalogNewsViewModel.MoreNewsCommand.Execute (null);
			}
			if(scrollView.ContentOffset.Y <= 0.0){
			}
		}
	}
}

