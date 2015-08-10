
using System;

using Foundation;
using UIKit;

namespace FSoft.WordApp.IOS.Views
{
	public partial class MenuAppInfoTableViewCell : MvxMDTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("MenuAppInfoTableViewCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("MenuAppInfoTableViewCell");

		public MenuAppInfoTableViewCell (IntPtr handle) : base (handle)
		{
		}

		public static MenuAppInfoTableViewCell Create ()
		{
			return (MenuAppInfoTableViewCell)Nib.Instantiate (null, null) [0];
		}

		public override UIEdgeInsets LayoutMargins {
			get {
				return UIEdgeInsets.Zero;
			}
			set {
				base.LayoutMargins = value;
			}
		}
	}
}

