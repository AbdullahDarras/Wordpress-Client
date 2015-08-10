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
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using FSoft.WordApp.Core.ViewModels;
using UIKit;
using System.Drawing;
using FSoft.WordApp.Core;

namespace FSoft.WordApp.IOS.Views
{
	public class BaseViewController : MvxViewController
	{
		public static UIColor COLOR_HIGHLIGHT = UIColor.White.ColorFromHex(Settings.COLOR_HIGHLIGHT);//[UIColor colorWithRed:0.247f green:0.639f blue:0.251f alpha:1.00f];//0x3FA340;
		public static UIColor COLOR_ACTIONBAR = UIColor.White.ColorFromHex(0x28388D);

		public BaseViewController (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			AppDelegate app = UIApplication.SharedApplication.Delegate as AppDelegate;

			//app.status
			this.NavigationController.NavigationBar.BarTintColor = COLOR_ACTIONBAR;
			this.NavigationController.NavigationBar.TintColor = UIColor.White;
			this.NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
//			this.NavigationController.NavigationBar.Translucent = true;

			//this.NavigationController.NavigationBar.SetBackgroundImage (new UIImage(), UIBarMetrics.Default);
			this.NavigationController.NavigationBar.BackgroundColor = COLOR_HIGHLIGHT;

			if (base.ViewModel is BaseViewModel) {
				((BaseViewModel)base.ViewModel).ErrorHandler += (sender, e) => {
					ErrorEventArgs ee = (ErrorEventArgs)e;
					UIAlertView alert = new UIAlertView (ee.Title, ee.Message, null, ee.CloseTitle, null);
					alert.Show ();
				};
			}
		}

		public override UIStatusBarStyle PreferredStatusBarStyle()
		{
			return UIStatusBarStyle.LightContent;
		}

		protected UILabel CreateNavTitle(string title) {
			UILabel lbNavTitle = new UILabel(new CoreGraphics.CGRect(60,40,320,40));//[[UILabel alloc] initWithFrame:CGRectMake(0,40,320,40)];
			lbNavTitle.TextAlignment = UITextAlignment.Left;
			lbNavTitle.Text = title;
			lbNavTitle.TextColor = UIColor.White;
			lbNavTitle.Font = UIFont.BoldSystemFontOfSize (20);
			return lbNavTitle;
		}

		protected UIView ViewToCenterOnKeyboardShown;
		public virtual bool HandlesKeyboardNotifications()
		{
			return false;
		}

		protected virtual void RegisterForKeyboardNotifications()
		{
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		protected virtual UIView KeyboardGetActiveView()
		{
			return View.FindFirstResponder();
		}

		private void OnKeyboardNotification (NSNotification notification)
		{
			if (!IsViewLoaded) return;

			//Check if the keyboard is becoming visible
			var visible = notification.Name == UIKeyboard.WillShowNotification;

			//Start an animation, using values from the keyboard
			UIView.BeginAnimations ("AnimateForKeyboard");
			UIView.SetAnimationBeginsFromCurrentState (true);
			UIView.SetAnimationDuration (UIKeyboard.AnimationDurationFromNotification (notification));
			UIView.SetAnimationCurve ((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification (notification));

			//Pass the notification, calculating keyboard height, etc.
			bool landscape = InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
			var keyboardFrame = visible
				? UIKeyboard.FrameEndFromNotification(notification)
				: UIKeyboard.FrameBeginFromNotification(notification);

			OnKeyboardChanged (visible, landscape ? (float)keyboardFrame.Width : (float)keyboardFrame.Height);

			//Commit the animation
			UIView.CommitAnimations ();
		}

		protected virtual void OnKeyboardChanged (bool visible, float keyboardHeight)
		{
			var activeView = ViewToCenterOnKeyboardShown ?? KeyboardGetActiveView();
			if (activeView == null)
				return;

			var scrollView = activeView.FindSuperviewOfType(View, typeof(UIScrollView)) as UIScrollView;
			if (scrollView == null)
				return;

			if (!visible)
				RestoreScrollPosition(scrollView);
			else
				CenterViewInScroll(activeView, scrollView, keyboardHeight);
		}


		protected virtual void CenterViewInScroll(UIView viewToCenter, UIScrollView scrollView, float keyboardHeight)
		{
//			var contentInsets = new UIEdgeInsets(0.0f, 0.0f, keyboardHeight, 0.0f);
//			scrollView.ContentInset = contentInsets;
//			scrollView.ScrollIndicatorInsets = contentInsets;
//
//			// Position of the active field relative isnside the scroll view
//			CoreGraphics.CGRect relativeFrame = viewToCenter.Superview.ConvertRectToView(viewToCenter.Frame, scrollView);
//
//			bool landscape = InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
//			var spaceAboveKeyboard = (landscape ? scrollView.Frame.Width : scrollView.Frame.Height) - keyboardHeight;
//
//			// Move the active field to the center of the available space
//			var offset = relativeFrame.Y - (spaceAboveKeyboard - viewToCenter.Frame.Height) / 2;
//			scrollView.ContentOffset = new CoreGraphics.CGPoint(0, offset);
		}

		protected virtual void RestoreScrollPosition(UIScrollView scrollView)
		{
//			scrollView.ContentInset = UIEdgeInsets.Zero;
//			scrollView.ScrollIndicatorInsets = UIEdgeInsets.Zero;
		}

		protected void DismissKeyboardOnBackgroundTap()
		{
			// Add gesture recognizer to hide keyboard
			var tap = new UITapGestureRecognizer { CancelsTouchesInView = false };
			tap.AddTarget(() => View.EndEditing(true));
			View.AddGestureRecognizer(tap);
		}
	}
}

