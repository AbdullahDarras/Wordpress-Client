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
using Cirrious.MvvmCross.Binding.BindingContext;
using FSoft.WordApp.Core;
using FSoft.WordApp.Core.Models;
using FSoft.WordApp.Core.ViewModels;
using ObjCRuntime;
using System.Drawing;
using CoreAnimation;

namespace FSoft.WordApp.iOS.Views
{
	public partial class PostView : BaseViewController, IUIScrollViewDelegate
	{
		public PostViewModel PostViewModel {get { return base.ViewModel as PostViewModel;}}

		private string _html;
		public string Html {
			get { return _html;}
			set { 
				_html = value;
				if (!string.IsNullOrEmpty (_html)) {
					this.NavigationItem.TitleView = CreateNavTitle (PostViewModel.Post.FirstCategoryName);//PostViewModel.Post.FirstCategoryName
					webViewPost.LoadHtmlString (_html,new NSUrl(Settings.BaseUrl));
				}
					
			}
		}

		private bool _isLikedThisPost;
		public bool IsLikedThisPost {
			get { return _isLikedThisPost;}
			set { 
				_isLikedThisPost = value;
				updateActionButtons ();
			}
		}
//		private UIView viewBottom;
		private bool isAnimatingBottomView;
		private bool isAddedBottomView = false;
		private bool isRemovedBottomView;

		public PostView () : base ("PostView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.NavigationBarHidden = false;
		}

		private void updateActionButtons(){
			if (PostViewModel.SupportLike) {
				if (IsLikedThisPost) {
					this.NavigationItem.SetRightBarButtonItems (new UIBarButtonItem[] {
						new UIBarButtonItem (UIImage.FromBundle ("comment")
						, UIBarButtonItemStyle.Plain
						, (sender, args) => {
							PostViewModel.ShowCommentCommand.Execute (null);

						}),
						new UIBarButtonItem (UIImage.FromBundle ("like")
						, UIBarButtonItemStyle.Plain
						, (sender, args) => {
							PostViewModel.UnLikeCommand.Execute (null);

						})
					}, true);
				} else {
					this.NavigationItem.SetRightBarButtonItems (new UIBarButtonItem[] {
						new UIBarButtonItem (UIImage.FromBundle ("comment")
						, UIBarButtonItemStyle.Plain
						, (sender, args) => {
							PostViewModel.ShowCommentCommand.Execute (null);

						}),
						new UIBarButtonItem (UIImage.FromBundle ("no-like")
						, UIBarButtonItemStyle.Plain
						, (sender, args) => {
							PostViewModel.LikeCommand.Execute (null);

						})
					}, true);
				}
			} else {
				this.NavigationItem.SetRightBarButtonItem (
					new UIBarButtonItem (UIImage.FromBundle ("comment")
						, UIBarButtonItemStyle.Plain
						, (sender, args) => {
						PostViewModel.ShowCommentCommand.Execute (null);

						}), true
				);
			}
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			AppDelegate app = UIApplication.SharedApplication.Delegate as AppDelegate;

			//this.NavigationItem.TitleView = CreateNavTitle (PostViewModel.Post.FirstCategoryName);

			this.NavigationController.NavigationBarHidden = false;

			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(UIImage.FromBundle("back")
					, UIBarButtonItemStyle.Plain
					, (sender, args) => {
						this.NavigationController.NavigationBarHidden = true;
						PostViewModel.BackCommand.Execute(null);
					}), true);	




			this.webViewPost.ScrollView.Delegate = this;
//			this.NavigationItem.SetRightBarButtonItem(
//				new UIBarButtonItem(UIImage.FromBundle("comment")
//					, UIBarButtonItemStyle.Plain
//					, (sender, args) => {
//						PostViewModel.ShowCommentCommand.Execute(null);
//
//					}), true);
//			
//			this.NavigationItem.SetRightBarButtonItem(
//				new UIBarButtonItem(UIImage.FromBundle("comment")
//					, UIBarButtonItemStyle.Plain
//					, (sender, args) => {
//						PostViewModel.ShowCommentCommand.Execute(null);
//
//					}), true);	
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.CreateBindingSet<PostView, PostViewModel>().Bind(this).For(v=>v.IsLikedThisPost).To(vm => vm.IsLikedThisPost).Apply();

			this.CreateBindingSet<PostView, PostViewModel>().Bind(this).For(v=>v.Html).To(vm => vm.Html).Apply();
			this.CreateBinding (lbLikeCount).For ("Text").To<PostViewModel> (vm => vm.Like_count).Apply ();
			this.CreateBinding (lbCommentCount).For ("Text").To<PostViewModel> (vm => vm.Comment_count).Apply ();

			this.CreateBinding (btnCommentBottom).For ("TouchUpInside").To<PostViewModel> (vm => vm.ShowCommentCommand).Apply ();
			this.CreateBinding (lbCommentCount).For("Tap").To<PostViewModel> (vm => vm.ShowCommentCommand).Apply ();
			this.CreateBinding (lbCommentBotom).For("Tap").To<PostViewModel> (vm => vm.ShowCommentCommand).Apply ();

			this.CreateBinding (btnLike).For ("TouchUpInside").To<PostViewModel> (vm => vm.LikeCommand).Apply ();
			this.CreateBinding (btnUnlike).For ("TouchUpInside").To<PostViewModel> (vm => vm.UnLikeCommand).Apply ();

			this.CreateBinding (pgrLoading).For ("Hidden").To<PostViewModel> (vm => vm.IsLoading).WithConversion ("Visibility").Apply ();
			this.CreateBinding (btnLike).For ("Hidden").To<PostViewModel> (vm => vm.IsUnLikedThisPost).WithConversion ("Visibility").Apply ();
			this.CreateBinding (btnUnlike).For ("Hidden").To<PostViewModel> (vm => vm.IsLikedThisPost).WithConversion ("Visibility").Apply ();


			webViewPost.ShouldStartLoad = (w, urlRequest, navigationType) => {
				if (navigationType == UIWebViewNavigationType.LinkClicked)
				{
					UIApplication.SharedApplication.OpenUrl(urlRequest.Url);
					return false;
				}
				return true;
			};
			//webViewPost.LoadHtmlString ();
			PostViewModel.LoadPost();

			Relayout ();
		}

		private void Relayout() {
			webViewPost.Frame = new CoreGraphics.CGRect (0,0,Settings.DeviceInfo.ScreenWidth,Settings.DeviceInfo.ScreenHeight);
			viewBottomDevider.Frame = new CoreGraphics.CGRect (0,0,Settings.DeviceInfo.ScreenWidth,2);
			viewBottomComment.Frame = new CoreGraphics.CGRect (Settings.DeviceInfo.ScreenWidth - viewBottomComment.Frame.Width, 8,viewBottomComment.Frame.Width,viewBottomComment.Frame.Height);

//			pgrLoading.Frame = new CoreGraphics.CGRect (Settings.DeviceInfo.ScreenWidth/2 - pgrLoading.Frame.Width/2,Settings.DeviceInfo.ScreenHeight/2-pgrLoading.Frame.Height/2,pgrLoading.Frame.Width,pgrLoading.Frame.Height);
			pgrLoading.Center = webViewPost.Center;
		}

		[Export ("scrollViewDidScroll:")]
		public void Scrolled (UIKit.UIScrollView scrollView)
		{
			if (scrollView.ContentOffset.Y >= (scrollView.ContentSize.Height - scrollView.Frame.Size.Height)) {
				if (isAnimatingBottomView)
					return;
				if (isAddedBottomView)
					return;
				System.Diagnostics.Debug.WriteLine ("Post WebView BOTTOM REACHED");
//				if (viewBottom == null) {
//					var arr = NSBundle.MainBundle.LoadNib ("PostViewBottomView", this, null);
//					viewBottom = Runtime.GetNSObject (arr.ValueAt (0)) as UIView;
//				}
				viewBottom.Frame = new CoreGraphics.CGRect (0, this.webViewPost.Frame.Bottom + 60, this.webViewPost.Frame.Width, 60);


				if (!isAddedBottomView)
					this.Add (viewBottom);
				
				isAddedBottomView = true;
				isAnimatingBottomView = true;

				UIView.Animate (0.5f, 0, UIViewAnimationOptions.CurveEaseOut,
					() => {
						//Animate (double duration, double delay, UIViewAnimationOptions options, Action animation, Action completion);

						viewBottom.Center = 
							new CoreGraphics.CGPoint (UIScreen.MainScreen.Bounds.Width/2, UIScreen.MainScreen.Bounds.Height - 30);
						isAnimatingBottomView = true;
					}, 
					() => {
						isAnimatingBottomView = false;
						viewBottom.Center = new CoreGraphics.CGPoint (UIScreen.MainScreen.Bounds.Width/2, UIScreen.MainScreen.Bounds.Height - 30); 
					}

				);

			} else {
				if (!isAddedBottomView)
					return;
				if (isAnimatingBottomView)
					return;
				isAddedBottomView = false;
				isAnimatingBottomView = true;

				System.Diagnostics.Debug.WriteLine("Post WebView BOTTOM UNREACHED");
				UIView.Animate (0.5f, 0, UIViewAnimationOptions.CurveEaseOut,
					() => {
						//Animate (double duration, double delay, UIViewAnimationOptions options, Action animation, Action completion);

						viewBottom.Center = 
							new CoreGraphics.CGPoint (UIScreen.MainScreen.Bounds.Width/2, UIScreen.MainScreen.Bounds.Height + 50);
						isAnimatingBottomView = true;
					}, 
					() => {
						isAnimatingBottomView = false;
						viewBottom.Center = new CoreGraphics.CGPoint (UIScreen.MainScreen.Bounds.Width/2, UIScreen.MainScreen.Bounds.Height + 50); 
					}

				);
			}
			if(scrollView.ContentOffset.Y <= 0.0){
				System.Diagnostics.Debug.WriteLine("Post WebView TOP REACHED");
			}
		}

		[Export("animationDidStop:finished:context:")]
		void SlideStopped (NSString animationID, NSNumber finished, NSObject context)
		{
			viewBottom.Center = new CoreGraphics.CGPoint (this.webViewPost.Frame.Width/2, this.webViewPost.Frame.Bottom - 50);;
		}
	}
}

