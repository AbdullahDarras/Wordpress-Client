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
using UIKit;
using Foundation;
using Cirrious.MvvmCross.Binding.Touch.Views;
using CoreGraphics;
using FSoft.WordApp.Core.Models;
using System.Drawing;
using FSoft.WordApp.Core;

namespace FSoft.WordApp.iOS.Views
{
	public class TableSourceComments : MvxTableViewSource
	{
		public TableSourceComments (UITableView tableView) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("PostCommentViewCell", NSBundle.MainBundle), PostCommentViewCell.Key);
		}

		//Define height of Cell follow by type data for cell view
		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var item = (Comment)GetItemAt (indexPath);
			String text = item.Content;
			nfloat twidth = tableView.Frame.Width - 67 - 16; //user + right padding
			nfloat height = TextHeight (text, twidth);

			//System.Diagnostics.Debug.WriteLine ("cellheight: {0} {1} {2}", height, twidth, text);

			return height + 32;

		}

		private nfloat TextHeight(string text, nfloat width) {
			nfloat height = text.StringSize (UIFont.SystemFontOfSize(14), new SizeF((float) width, float.MaxValue), UILineBreakMode.WordWrap).Height;
			//text.StringSize
//			NSString s = new NSString(text);
//			height = s.GetBoundingRect (new SizeF (width, float.MaxValue), NSStringDrawingOptions.UsesFontLeading | NSStringDrawingOptions.UsesLineFragmentOrigin, new NSStringDrawingContext()).Height;
			return height;
//			return 300;
		}

		//Define type CellView
		protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
		{
			//Default is top cell
			UIColor bgUnselected;
			UIView bgSelected=new UIView();
			bgUnselected = UIColor.Clear;
			bgSelected.BackgroundColor = UIColor.Clear;

			NSString identifier = PostCommentViewCell.Key;


			var cell = (PostCommentViewCell)tableView.DequeueReusableCell(identifier, indexPath);
			cell.AutosizesSubviews = true;
			var imgC = cell.ViewWithTag (100);
			var ct = cell.ViewWithTag (101); //comment content
			if (ct != null && ct is UILabel) {
				var tt = cell.ViewWithTag (102);//title
				((UILabel)ct).Frame = new CGRect(tt.Frame.Left,tt.Frame.Bottom + 5, cell.Frame.Width - 67 - 16, TextHeight (((Comment)item).Content, cell.Frame.Width - 67 - 16));
			}
			if (imgC != null) {
				setRoundedView (imgC, (float) imgC.Frame.Width);
				var c = (Comment)item;
				int color_code = 0;
				if (c.Author!= null) color_code = c.Author.id % 5;

				if (color_code == 0) {
					imgC.BackgroundColor = ColorFromHex(0x4DD0E1);
				} else if (color_code == 1) {
					imgC.BackgroundColor = ColorFromHex(0xFFB74D);
				} else if (color_code == 2) {
					imgC.BackgroundColor = ColorFromHex(0x81C784);
				} else if (color_code == 3) {
					imgC.BackgroundColor = ColorFromHex(0xF06292);
				} else if (color_code == 4) {
					imgC.BackgroundColor = ColorFromHex(0x9575CD);
				}

				UIView v = cell.ViewWithTag (130);//time ago
				v.SetRight(Settings.DeviceInfo.ScreenWidth - 8);
			}
				

			cell.BackgroundColor = bgUnselected;
			cell.SelectedBackgroundView = bgSelected;

			return cell;
		}

		public UIColor ColorFromHex(int hexValue) {
			return UIColor.FromRGB(
				(((float)((hexValue & 0xFF0000) >> 16))/255.0f),
				(((float)((hexValue & 0xFF00) >> 8))/255.0f),
				(((float)(hexValue & 0xFF))/255.0f)
			);
		}

		public void setRoundedView(UIView roundedView, float newSize)
		{
			CGPoint saveCenter = roundedView.Center;
			CGRect newFrame = new CGRect(roundedView.Frame.X, roundedView.Frame.Y, newSize, newSize);//CGRectMake(roundedView.frame.origin.x, roundedView.frame.origin.y, newSize, newSize);
			roundedView.Frame = newFrame;
			roundedView.Layer.CornerRadius = newSize / (float)2.0;
			roundedView.Center = saveCenter;
		}
	}
}

