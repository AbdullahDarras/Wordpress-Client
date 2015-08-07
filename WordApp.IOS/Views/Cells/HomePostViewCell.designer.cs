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

﻿// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FSoft.WordApp.iOS.Views
{
	[Register ("HomePostViewCell")]
	partial class HomePostViewCell
	{
		[Outlet]
		UIKit.UIImageView imgPostThumb { get; set; }

		[Outlet]
		UIKit.UILabel lbPostTime { get; set; }

		[Outlet]
		UIKit.UILabel lbPostTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imgPostThumb != null) {
				imgPostThumb.Dispose ();
				imgPostThumb = null;
			}

			if (lbPostTitle != null) {
				lbPostTitle.Dispose ();
				lbPostTitle = null;
			}

			if (lbPostTime != null) {
				lbPostTime.Dispose ();
				lbPostTime = null;
			}
		}
	}
}
