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

namespace FSoft.WordApp.iOS.Control
{
	[Register ("AlignLabel")]
	public class AlignLabel : UILabel
	{
		public AlignLabel ()
		{
		}

		public AlignLabel(IntPtr handle)
			: base(handle)
		{}

		public override void Draw (CoreGraphics.CGRect rect)
		{
			base.Draw (rect);
		}

		public override CoreGraphics.CGRect Bounds {
			get {
				return base.Bounds;
			}
			set {
				base.Bounds = value;
				if (base.Lines == 0 && Bounds.Size.Width != PreferredMaxLayoutWidth) {
					PreferredMaxLayoutWidth = Bounds.Size.Width;
					SetNeedsUpdateConstraints();
				}
			}
		}
		public override string Text {
			get {
				return base.Text;
			}
			set {
				base.Text = value;
				SizeToFit ();
			}
		}
	}
}

