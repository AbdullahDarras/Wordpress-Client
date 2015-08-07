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
	[Register ("MenuView")]
	partial class MenuView
	{
		[Outlet]
		iOSUILib.MDButton btnSignout { get; set; }

		[Outlet]
		UIKit.UILabel lblUserCaption { get; set; }

		[Outlet]
		UIKit.UITableView tableViewCategories { get; set; }

		[Outlet]
		UIKit.UILabel txtUserEmail { get; set; }

		[Outlet]
		UIKit.UILabel txtUsername { get; set; }

		[Outlet]
		UIKit.UIView viewSignout { get; set; }

		[Outlet]
		UIKit.UIView viewSignoutDivider { get; set; }

		[Outlet]
		UIKit.UIView viewUserCaption { get; set; }

		[Outlet]
		UIKit.UIView viewUserInfo { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSignout != null) {
				btnSignout.Dispose ();
				btnSignout = null;
			}

			if (lblUserCaption != null) {
				lblUserCaption.Dispose ();
				lblUserCaption = null;
			}

			if (tableViewCategories != null) {
				tableViewCategories.Dispose ();
				tableViewCategories = null;
			}

			if (viewUserInfo != null) {
				viewUserInfo.Dispose ();
				viewUserInfo = null;
			}

			if (txtUserEmail != null) {
				txtUserEmail.Dispose ();
				txtUserEmail = null;
			}

			if (txtUsername != null) {
				txtUsername.Dispose ();
				txtUsername = null;
			}

			if (viewUserCaption != null) {
				viewUserCaption.Dispose ();
				viewUserCaption = null;
			}

			if (viewSignout != null) {
				viewSignout.Dispose ();
				viewSignout = null;
			}

			if (viewSignoutDivider != null) {
				viewSignoutDivider.Dispose ();
				viewSignoutDivider = null;
			}
		}
	}
}
