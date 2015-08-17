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
	[Register ("PostCommentsView")]
	partial class PostCommentsView
	{
		[Outlet]
		MaterialControls.MDButton btnPostComment { get; set; }

		[Outlet]
		UIKit.UIImageView imgNoComment { get; set; }

		[Outlet]
		UIKit.UILabel lbNoComment { get; set; }

		[Outlet]
		UIKit.UILabel lbPostTitle { get; set; }

		[Outlet]
		MaterialControls.MDProgress prgLoading { get; set; }

		[Outlet]
		UIKit.UITableView tableComments { get; set; }

		[Outlet]
		MaterialControls.MDTextField txtComment { get; set; }

		[Outlet]
		MaterialControls.MDTextField txtEmail { get; set; }

		[Outlet]
		MaterialControls.MDTextField txtName { get; set; }

		[Outlet]
		UIKit.UIView viewCommentTextAndSend { get; set; }

		[Outlet]
		UIKit.UIView viewInputComment { get; set; }

		[Outlet]
		UIKit.UIView viewNoComment { get; set; }

		[Outlet]
		UIKit.UIView viewNoCommentDevider { get; set; }

		[Outlet]
		UIKit.UIView viewPostCommentNameAndEmail { get; set; }

		[Outlet]
		UIKit.UIView viewTxtCommentDivider { get; set; }

		[Outlet]
		UIKit.UIView viewTxtEmailDivider { get; set; }

		[Outlet]
		UIKit.UIView viewTxtNameDivider { get; set; }
		
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

			if (txtEmail != null) {
				txtEmail.Dispose ();
				txtEmail = null;
			}

			if (txtName != null) {
				txtName.Dispose ();
				txtName = null;
			}

			if (viewInputComment != null) {
				viewInputComment.Dispose ();
				viewInputComment = null;
			}

			if (viewNoComment != null) {
				viewNoComment.Dispose ();
				viewNoComment = null;
			}

			if (viewCommentTextAndSend != null) {
				viewCommentTextAndSend.Dispose ();
				viewCommentTextAndSend = null;
			}

			if (viewNoCommentDevider != null) {
				viewNoCommentDevider.Dispose ();
				viewNoCommentDevider = null;
			}

			if (viewPostCommentNameAndEmail != null) {
				viewPostCommentNameAndEmail.Dispose ();
				viewPostCommentNameAndEmail = null;
			}

			if (viewTxtCommentDivider != null) {
				viewTxtCommentDivider.Dispose ();
				viewTxtCommentDivider = null;
			}

			if (viewTxtEmailDivider != null) {
				viewTxtEmailDivider.Dispose ();
				viewTxtEmailDivider = null;
			}

			if (viewTxtNameDivider != null) {
				viewTxtNameDivider.Dispose ();
				viewTxtNameDivider = null;
			}
		}
	}
}
