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
using FSoft.WordApp.Core.Models;

namespace FSoft.WordApp.IOS.Views
{
	public class TableSourceMenu : MvxTableViewSource
	{
		public TableSourceMenu (UITableView tableView) : base(tableView)
		{
//			tableView.Frame.Width = 375;
//			tableView.Frame.Height = 667;

			tableView.Frame = new CoreGraphics.CGRect (0,0,375,667);


			tableView.RegisterNibForCellReuse(UINib.FromName("MenuAvatarTableViewCell", NSBundle.MainBundle), MenuAvatarTableViewCell.Key);
			tableView.RegisterNibForCellReuse(UINib.FromName("MenuCategory", NSBundle.MainBundle), MenuCategoryViewCell.Key);
			tableView.RegisterNibForCellReuse(UINib.FromName("MenuSubCategory", NSBundle.MainBundle), MenuSubCategoryCellView.Key);
			tableView.RegisterNibForCellReuse(UINib.FromName("MenuSigtoutViewCell", NSBundle.MainBundle), MenuSigtoutViewCell.Key);
			tableView.RegisterNibForCellReuse(UINib.FromName("MenuAppInfoTableViewCell", NSBundle.MainBundle), MenuAppInfoTableViewCell.Key);
		}

		//Define height of Cell follow by type data for cell view
		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var item = GetItemAt (indexPath);
			if (item is AppInfoOptionItem) {
				return 100;
			}
			return 50;

		}


		//Define type CellView
		protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
		{
			//Default is top cell
			UIColor bgUnselected;
			UIView bgSelected=new UIView();
			bgUnselected = UIColor.Clear;
			bgSelected.BackgroundColor = UIColor.Clear;

			NSString identifier = MenuCategoryViewCell.Key;
			if (item is OptionItem) {
				if (item is CategoryOptionItem) {
					var cat = (item as CategoryOptionItem).Category;
					if (cat.Parent == 0) {
						identifier = MenuCategoryViewCell.Key;
//						bgSelected.BackgroundColor = UIColor.FromRGB (255,188,167);
					} else {
						identifier = MenuSubCategoryCellView.Key;
//						bgSelected.BackgroundColor = UIColor.FromRGB (255,188,167);
					}
				} else if (item is SignoutOptionItem) {
					identifier = MenuSigtoutViewCell.Key;
				}  else if (item is AppInfoOptionItem) {
					identifier = MenuAppInfoTableViewCell.Key;
				}
			} else  {
				
			}
			var cell = (UITableViewCell)tableView.DequeueReusableCell(identifier, indexPath);

			cell.BackgroundColor = bgUnselected;
			cell.SelectedBackgroundView = bgSelected;
			return cell;
		}
	}
}

