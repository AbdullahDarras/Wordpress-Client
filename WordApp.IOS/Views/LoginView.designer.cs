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

namespace FSoft.WordApp.IOS.Views
{
	[Register ("LoginView")]
	partial class LoginView
	{
		[Outlet]
		iOSUILib.MDButton btnLogin { get; set; }

		[Outlet]
		UIKit.UIImageView imgLogo { get; set; }

		[Outlet]
		UIKit.UILabel lbCopyright { get; set; }

		[Outlet]
		UIKit.UILabel lbRememberPwd { get; set; }

		[Outlet]
		UIKit.UIProgressView prgLoading { get; set; }

		[Outlet]
		iOSUILib.MDSwitch swRememberPassword { get; set; }

		[Outlet]
		UIKit.UITextField txtPassword { get; set; }

		[Outlet]
		UIKit.UITextField txtUsername { get; set; }

		[Outlet]
		UIKit.UIView viewInfo { get; set; }

		[Outlet]
		UIKit.UIView viewInput { get; set; }

		[Outlet]
		UIKit.UIView viewTxtPwdDivider { get; set; }

		[Outlet]
		UIKit.UIView viewTxtUsernameDivider { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}

			if (imgLogo != null) {
				imgLogo.Dispose ();
				imgLogo = null;
			}

			if (lbRememberPwd != null) {
				lbRememberPwd.Dispose ();
				lbRememberPwd = null;
			}

			if (prgLoading != null) {
				prgLoading.Dispose ();
				prgLoading = null;
			}

			if (swRememberPassword != null) {
				swRememberPassword.Dispose ();
				swRememberPassword = null;
			}

			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}

			if (txtUsername != null) {
				txtUsername.Dispose ();
				txtUsername = null;
			}

			if (viewInfo != null) {
				viewInfo.Dispose ();
				viewInfo = null;
			}

			if (viewInput != null) {
				viewInput.Dispose ();
				viewInput = null;
			}

			if (viewTxtPwdDivider != null) {
				viewTxtPwdDivider.Dispose ();
				viewTxtPwdDivider = null;
			}

			if (viewTxtUsernameDivider != null) {
				viewTxtUsernameDivider.Dispose ();
				viewTxtUsernameDivider = null;
			}

			if (lbCopyright != null) {
				lbCopyright.Dispose ();
				lbCopyright = null;
			}
		}
	}
}
