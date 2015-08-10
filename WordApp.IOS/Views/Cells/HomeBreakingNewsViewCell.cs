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
using Cirrious.MvvmCross.Binding.BindingContext;
using FSoft.WordApp.Core.ViewModels;
using FSoft.WordApp.Core.Models;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace FSoft.WordApp.IOS.Views
{
	public partial class HomeBreakingNewsViewCell : MvxMDTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("HomeBreakingNewsViewCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("HomeBreakingNewsViewCell");

		public HomeBreakingNewsViewCell (IntPtr handle) : base (handle)
		{
			this.DelayBind(() => {
				this.CreateBindingSet<HomeBreakingNewsViewCell, BreakingNews>().Bind(lbPostTitle).To(item => item.Title_plain).Apply();
				var pnlBackImageLoader = new MvxImageViewLoader(() => imgPostThumb);
				this.CreateBindingSet<HomeBreakingNewsViewCell, BreakingNews > ().Bind(pnlBackImageLoader).To(item=>item.IconSource).Apply();
			});
		}

		public static HomeBreakingNewsViewCell Create ()
		{
			return (HomeBreakingNewsViewCell)Nib.Instantiate (null, null) [0];
		}
	}
}

