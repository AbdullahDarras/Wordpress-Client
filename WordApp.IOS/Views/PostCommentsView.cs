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

﻿
using System;

using Foundation;
using UIKit;
using FSoft.WordApp.Core.Models;
using FSoft.WordApp.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using FSoft.WordApp.Core;


namespace FSoft.WordApp.IOS.Views
{
	public partial class PostCommentsView : BaseViewController, IUITextFieldDelegate
	{
		public PostCommentsViewModel PostCommentsViewModel {get { return base.ViewModel as PostCommentsViewModel;}}

		private bool isButtonActived;
		private UIImage imgButtonActive = UIImage.FromBundle("send button active.png");
		private UIImage imgButtonInactive = UIImage.FromBundle("send button.png");
		public PostCommentsView () : base ("PostCommentsView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
			if (PostCommentsViewModel == null) return;

			RegisterForKeyboardNotifications ();

			this.NavigationItem.TitleView = CreateNavTitle ("Comments");


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("back")
					, UIBarButtonItemStyle.Plain
					, (sender, args) => {
						this.NavigationController.NavigationBarHidden = true;
						PostCommentsViewModel.BackCommand.Execute(null);
					}), true);
			this.NavigationItem.SetRightBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("refresh")
					, UIBarButtonItemStyle.Plain
					, (sender, args) => {
						PostCommentsViewModel.RefreshCommentsCommand.Execute(null);
					}), true);

//			this.CreateBindingSet<PostCommentsView, PostCommentsViewModel> ().Bind (this).For (v => v.CommentText).To (vm => vm.CommentText).Apply ();

			var TableSourceComments = new TableSourceComments (tableComments);
			this.CreateBinding (TableSourceComments).To<PostCommentsViewModel> (vm => vm.Comments).Apply ();
			tableComments.Source = TableSourceComments;
			tableComments.ReloadData ();

			this.CreateBinding (viewNoComment).For ("Hidden").To<PostCommentsViewModel> (vm => vm.IsNoComment).WithConversion ("Visibility").Apply ();
			this.CreateBinding (tableComments).For ("Hidden").To<PostCommentsViewModel> (vm => vm.HasComment).WithConversion ("Visibility").Apply ();
			this.CreateBinding (prgLoading).For ("Hidden").To<PostCommentsViewModel> (vm => vm.IsLoading).WithConversion ("Visibility").Apply ();

			this.CreateBinding(lbPostTitle).For("Text").To<PostCommentsViewModel> (vm => vm.NewsTitle).Apply();

			this.CreateBinding (txtComment).For ("Text").To<PostCommentsViewModel> (vm => vm.CommentText).Apply ();
			this.CreateBinding (txtName).For ("Text").To<PostCommentsViewModel> (vm => vm.CommentName).Apply ();
			this.CreateBinding (txtEmail).For ("Text").To<PostCommentsViewModel> (vm => vm.CommentEmail).Apply ();

			this.CreateBinding (btnPostComment).For ("TouchUpInside").To<PostCommentsViewModel> (vm => vm.PostCommentCommand).Apply ();

			isButtonActived = false;
			txtComment.EditingChanged += (sender, e) => {
				UITextField t = (UITextField)sender;

				if ( string.IsNullOrEmpty(t.Text)) {
					isButtonActived = false;
					btnPostComment.ImageView.Image = imgButtonInactive;
				} else if (!isButtonActived){
					isButtonActived = true;
					btnPostComment.ImageView.Image = imgButtonActive;
				}
			};

			txtComment.EditingDidBegin += (object sender, EventArgs e) => {
				viewTxtCommentDivider.BackgroundColor = COLOR_HIGHLIGHT;
			};
			txtComment.EditingDidEnd += (object sender, EventArgs e) => {
				viewTxtCommentDivider.BackgroundColor = UIColor.LightGray;
			};

			txtName.EditingDidBegin += (object sender, EventArgs e) => {
				viewTxtNameDivider.BackgroundColor = COLOR_HIGHLIGHT;
			};
			txtName.EditingDidEnd += (object sender, EventArgs e) => {
				viewTxtNameDivider.BackgroundColor = UIColor.LightGray;
			};

			txtEmail.EditingDidBegin += (object sender, EventArgs e) => {
				viewTxtEmailDivider.BackgroundColor = COLOR_HIGHLIGHT;
			};
			txtEmail.EditingDidEnd += (object sender, EventArgs e) => {
				viewTxtEmailDivider.BackgroundColor = UIColor.LightGray;
			};

//			txtComment.Delegate = this;
			this.PostCommentsViewModel.PostCommentPressed += (sender, e) => {
				View.EndEditing(true);
				if (!(e is ErrorEventArgs)) {
					
				}
				tableComments.ContentOffset = new CoreGraphics.CGPoint(0, nfloat.MaxValue);//scroll to bottom
			};
			DismissKeyboardOnBackgroundTap ();

			Relayout ();
		}

		private void Relayout() {
			prgLoading.Center = new CoreGraphics.CGPoint(Settings.DeviceInfo.ScreenWidth/2, Settings.DeviceInfo.ScreenHeight/2);

			viewNoComment.SetWidth(Settings.DeviceInfo.ScreenWidth);
			lbPostTitle.SetWidth (Settings.DeviceInfo.ScreenWidth - 37 -8);
			viewNoCommentDevider.SetWidth (Settings.DeviceInfo.ScreenWidth);
			imgNoComment.SetCenterX (Settings.DeviceInfo.ScreenWidth / 2);
			lbNoComment.SetCenterX (Settings.DeviceInfo.ScreenWidth / 2);

			viewInputComment.SetBottom (Settings.DeviceInfo.ScreenHeight);
			viewInputComment.SetWidth(Settings.DeviceInfo.ScreenWidth);

			btnPostComment.SetRight (Settings.DeviceInfo.ScreenWidth - 8);
			txtComment.SetWidth (Settings.DeviceInfo.ScreenWidth - btnPostComment.Frame.Width - 16);
			viewTxtCommentDivider.SetWidth (Settings.DeviceInfo.ScreenWidth - btnPostComment.Frame.Width - 16);

			//add name and email field
			if (!Settings.wpLoggedIn) {
				viewInputComment.SetHeight (150);
				txtName.SetWidth (Settings.DeviceInfo.ScreenWidth - btnPostComment.Frame.Width - 16);
				txtEmail.SetWidth (Settings.DeviceInfo.ScreenWidth - btnPostComment.Frame.Width - 16);
			} else {
				viewInputComment.SetHeight (50);
			}


			tableComments.Frame = new CoreGraphics.CGRect (tableComments.Frame.Left, tableComments.Frame.Top, Settings.DeviceInfo.ScreenWidth, viewInputComment.Frame.Top - tableComments.Frame.Top);
		}


		protected override  void OnKeyboardChanged (bool visible, float keyboardHeight)
		{
			//View.LayoutSubviews ();
			base.OnKeyboardChanged(visible, keyboardHeight);
			if (visible) {
				txtComment.BecomeFirstResponder ();
				viewInputComment.Frame = new CoreGraphics.CGRect(0,UIScreen.MainScreen.Bounds.Height - viewInputComment.Frame.Height - keyboardHeight, viewInputComment.Frame.Width, viewInputComment.Frame.Height);
			} else {
				viewInputComment.Frame = new CoreGraphics.CGRect(0,UIScreen.MainScreen.Bounds.Height - viewInputComment.Frame.Height,viewInputComment.Frame.Width, viewInputComment.Frame.Height);
			}
			//viewInputComment.LayoutSubviews ();
		}

//		public virtual bool ShouldReturn (UITextField textField)
//		{
//			textField.ResignFirstResponder ();
//			dis
//			return true;
//		}
	}
}

