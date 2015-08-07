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
using FSoft.WordApp.Core;
using FSoft.WordApp.Core.Models;
using FSoft.WordApp.Core.ViewModels;

namespace FSoft.WordApp.iOS.Views
{
	public class TableSourceHomePosts : MvxTableViewSource
	{
		public TableSourceHomePosts (UITableView tableView) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("HomeBreakingNewsViewCell", NSBundle.MainBundle), HomeBreakingNewsViewCell.Key);
			tableView.RegisterNibForCellReuse(UINib.FromName("HomeCategoryViewCell", NSBundle.MainBundle), HomeCategoryViewCell.Key);
			tableView.RegisterNibForCellReuse(UINib.FromName("HomePostViewCell", NSBundle.MainBundle), HomePostViewCell.Key);
		}

		//Define height of Cell follow by type data for cell view
		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			nfloat size = 110;
			var item = GetItemAt (indexPath);
			if (item is BreakingNews) {
				size = 150;
			} else if (item is Post) {
				size = 86;
			} else if (item is Category) {
				size = 52;
			}
			return size;

		}


		//Define type CellView
		protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
		{
			//Default is top cell
			UIColor bgUnselected;
			UIView bgSelected=new UIView();
			bgUnselected = UIColor.White;
			bgSelected.BackgroundColor = UIColor.White;

			NSString identifier = HomePostViewCell.Key;
			if (item is BreakingNews) {
				identifier = HomeBreakingNewsViewCell.Key;
			} else if (item is Post) {
				identifier = HomePostViewCell.Key;
			} else if (item is Category) {
				identifier = HomeCategoryViewCell.Key;
			}
					

			var cell = (UITableViewCell)tableView.DequeueReusableCell(identifier, indexPath);

			if (item is BreakingNews) {
				UIView v = cell.ViewWithTag (110);//thumb
				v.Frame = new CoreGraphics.CGRect (v.Frame.Left, v.Frame.Top, Settings.DeviceInfo.ScreenWidth, 150);//v.Frame.Height
				v = cell.ViewWithTag (111);//overlay
				v.Frame = new CoreGraphics.CGRect (v.Frame.Left, v.Frame.Top, Settings.DeviceInfo.ScreenWidth, v.Frame.Height);
				v = cell.ViewWithTag (112);//title
				//v.SizeToFit();
				v.Frame = new CoreGraphics.CGRect (v.Frame.Left, v.Frame.Top, Settings.DeviceInfo.ScreenWidth - 3 * Settings.PADDING_IOS, v.Frame.Height);
			} else if (item is Post) {
				UIView v = cell.ViewWithTag (113);//title
				v.Frame = new CoreGraphics.CGRect (v.Frame.Left, v.Frame.Top, Settings.DeviceInfo.ScreenWidth - 120 - 3 * Settings.PADDING_IOS, v.Frame.Height);

			} else if (item is Category) {
				identifier = HomeCategoryViewCell.Key;
			}

			cell.BackgroundColor = bgUnselected;
			cell.SelectedBackgroundView = bgSelected;
			return cell;
		}
	}
}

