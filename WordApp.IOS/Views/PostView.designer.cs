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
	[Register ("PostView")]
	partial class PostView
	{
		[Outlet]
		UIKit.UIButton btnCommentBottom { get; set; }

		[Outlet]
		UIKit.UIButton btnLike { get; set; }

		[Outlet]
		UIKit.UIButton btnUnlike { get; set; }

		[Outlet]
		UIKit.UILabel lbCommentBotom { get; set; }

		[Outlet]
		UIKit.UILabel lbCommentCount { get; set; }

		[Outlet]
		UIKit.UILabel lbLikeCount { get; set; }

		[Outlet]
		UIKit.UIProgressView pgrLoading { get; set; }

		[Outlet]
		UIKit.UIView viewBottom { get; set; }

		[Outlet]
		UIKit.UIView viewBottomComment { get; set; }

		[Outlet]
		UIKit.UIView viewBottomDevider { get; set; }

		[Outlet]
		UIKit.UIWebView webViewPost { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCommentBottom != null) {
				btnCommentBottom.Dispose ();
				btnCommentBottom = null;
			}

			if (btnLike != null) {
				btnLike.Dispose ();
				btnLike = null;
			}

			if (btnUnlike != null) {
				btnUnlike.Dispose ();
				btnUnlike = null;
			}

			if (lbCommentBotom != null) {
				lbCommentBotom.Dispose ();
				lbCommentBotom = null;
			}

			if (lbCommentCount != null) {
				lbCommentCount.Dispose ();
				lbCommentCount = null;
			}

			if (lbLikeCount != null) {
				lbLikeCount.Dispose ();
				lbLikeCount = null;
			}

			if (pgrLoading != null) {
				pgrLoading.Dispose ();
				pgrLoading = null;
			}

			if (viewBottom != null) {
				viewBottom.Dispose ();
				viewBottom = null;
			}

			if (viewBottomDevider != null) {
				viewBottomDevider.Dispose ();
				viewBottomDevider = null;
			}

			if (viewBottomComment != null) {
				viewBottomComment.Dispose ();
				viewBottomComment = null;
			}

			if (webViewPost != null) {
				webViewPost.Dispose ();
				webViewPost = null;
			}
		}
	}
}
