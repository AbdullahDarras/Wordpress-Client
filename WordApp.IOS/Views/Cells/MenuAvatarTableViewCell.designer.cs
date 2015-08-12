// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FSoft.WordApp.IOS.Views
{
	[Register ("MenuAvatarTableViewCell")]
	partial class MenuAvatarTableViewCell
	{
		[Outlet]
		UIKit.UILabel lbUserAvatarCaption { get; set; }

		[Outlet]
		UIKit.UILabel lbUserEmail { get; set; }

		[Outlet]
		UIKit.UILabel lbUserName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lbUserAvatarCaption != null) {
				lbUserAvatarCaption.Dispose ();
				lbUserAvatarCaption = null;
			}

			if (lbUserEmail != null) {
				lbUserEmail.Dispose ();
				lbUserEmail = null;
			}

			if (lbUserName != null) {
				lbUserName.Dispose ();
				lbUserName = null;
			}
		}
	}
}
