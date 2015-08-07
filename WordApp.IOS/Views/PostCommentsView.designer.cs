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
	[Register ("PostCommentsView")]
	partial class PostCommentsView
	{
		[Outlet]
		iOSUILib.MDButton btnPostComment { get; set; }

		[Outlet]
		UIKit.UIImageView imgNoComment { get; set; }

		[Outlet]
		UIKit.UILabel lbNoComment { get; set; }

		[Outlet]
		UIKit.UILabel lbPostTitle { get; set; }

		[Outlet]
		iOSUILib.MDProgress prgLoading { get; set; }

		[Outlet]
		UIKit.UITableView tableComments { get; set; }

		[Outlet]
		iOSUILib.MDTextField txtComment { get; set; }

		[Outlet]
		UIKit.UIView viewInputComment { get; set; }

		[Outlet]
		UIKit.UIView viewNoComment { get; set; }

		[Outlet]
		UIKit.UIView viewNoCommentDevider { get; set; }

		[Outlet]
		UIKit.UIView viewTxtCommentDivider { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnPostComment != null) {
				btnPostComment.Dispose ();
				btnPostComment = null;
			}

			if (imgNoComment != null) {
				imgNoComment.Dispose ();
				imgNoComment = null;
			}

			if (lbNoComment != null) {
				lbNoComment.Dispose ();
				lbNoComment = null;
			}

			if (lbPostTitle != null) {
				lbPostTitle.Dispose ();
				lbPostTitle = null;
			}

			if (prgLoading != null) {
				prgLoading.Dispose ();
				prgLoading = null;
			}

			if (tableComments != null) {
				tableComments.Dispose ();
				tableComments = null;
			}

			if (txtComment != null) {
				txtComment.Dispose ();
				txtComment = null;
			}

			if (viewInputComment != null) {
				viewInputComment.Dispose ();
				viewInputComment = null;
			}

			if (viewNoComment != null) {
				viewNoComment.Dispose ();
				viewNoComment = null;
			}

			if (viewNoCommentDevider != null) {
				viewNoCommentDevider.Dispose ();
				viewNoCommentDevider = null;
			}

			if (viewTxtCommentDivider != null) {
				viewTxtCommentDivider.Dispose ();
				viewTxtCommentDivider = null;
			}
		}
	}
}
