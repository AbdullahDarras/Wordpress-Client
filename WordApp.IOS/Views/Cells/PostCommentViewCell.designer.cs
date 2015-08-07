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

using Foundation;
using System.CodeDom.Compiler;

namespace FSoft.WordApp.iOS.Views
{
	[Register ("PostCommentViewCell")]
	partial class PostCommentViewCell
	{
		[Outlet]
		UIKit.UILabel lbComment { get; set; }

		[Outlet]
		UIKit.UILabel lbTime { get; set; }

		[Outlet]
		UIKit.UILabel lbUserCaption { get; set; }

		[Outlet]
		UIKit.UILabel lbUsername { get; set; }

		[Outlet]
		UIKit.UIView viewUserCaption { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (viewUserCaption != null) {
				viewUserCaption.Dispose ();
				viewUserCaption = null;
			}

			if (lbUserCaption != null) {
				lbUserCaption.Dispose ();
				lbUserCaption = null;
			}

			if (lbUsername != null) {
				lbUsername.Dispose ();
				lbUsername = null;
			}

			if (lbTime != null) {
				lbTime.Dispose ();
				lbTime = null;
			}

			if (lbComment != null) {
				lbComment.Dispose ();
				lbComment = null;
			}
		}
	}
}
