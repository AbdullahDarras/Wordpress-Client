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

﻿using Android.OS;
using Cirrious.MvvmCross.Droid.FullFragging.Fragments;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using Android.Views;
using Android.Webkit;

using FSoft.WordApp.Core.ViewModels;
using Cirrious.CrossCore.Platform;
using Android.App;
using Cirrious.MvvmCross.Droid.Views;
using Android.Animation;
using System;
using Android.Views.InputMethods;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Android.Content;
using Android.Util;

namespace FSoft.WordApp.Droid
{
	[Activity (Label = "Post", MainLauncher = false, Theme = "@style/Theme.AppCompat.Light.NoActionBar")]

	public class PostView : MvxActivity
	{
		private PostViewModel PostViewModel { get { return base.ViewModel as PostViewModel;}}
		private View CommentsView;
		private View InputCommentEditText;
		private bool IsCommentsViewAnimating;
		private bool IsBottomViewAnimating;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.OverridePendingTransition(Resource.Animation.slide_right,Resource.Animation.slide_left);
			View _view = this.BindingInflate(Resource.Layout.PostView, null);

			SetContentView (_view);

			ObservableWebview web = (ObservableWebview)this.FindViewById (Resource.Id.post_view_webview);
			WebSettings settings = web.Settings;
			settings.DefaultTextEncodingName = "utf-8";


			PostViewModel.PropertyChanged += (sender, e) => {
				if(e.PropertyName == "Html") {
					web.LoadData (((PostViewModel)base.ViewModel).Html,"text/html; charset=utf-8","utf-8");
				}
			};

			CommentsView = _view.FindViewById (Resource.Id.post_view_comments_layout);
			InputCommentEditText = _view.FindViewById (Resource.Id.post_view_comments_editText);

			PostViewModel.CommentPressed += (sender, e) => {
				if (IsCommentsViewAnimating) return;
				if(CommentsView.Visibility == ViewStates.Visible){
					//hide softkeyboard
					HideSoftkeyboard ();
					//hide
					IsCommentsViewAnimating = true;
					var al = new AnimatorListener();
					al.OnAnimationEndAction  = (Animator animation) => {
						CommentsView.Visibility = ViewStates.Gone;
						IsCommentsViewAnimating = false;
					};
					CommentsView.Animate().TranslationX(CommentsView.Width).SetListener(al).SetDuration(200);
				} else {
					//show
					IsCommentsViewAnimating = true;
					var al = new AnimatorListener();
					al.OnAnimationEndAction  = (Animator animation) => {
						IsCommentsViewAnimating = false;
					};
					if (CommentsView.TranslationX < _view.Width){
						//width of the view is zero before layout
						CommentsView.TranslationX = _view.Width; //set to right
					}
					CommentsView.Visibility = ViewStates.Visible;
					CommentsView.Animate().TranslationX(0).SetListener(al).SetDuration(200);
				}
			};

			PostViewModel.PostCommentPressed += (sender, e) => {
				HideSoftkeyboard ();

				((EditText)InputCommentEditText).Text = string.Empty;//using binding
			};

			PostViewModel.ErrorHandler += (sender, e) => {
				ErrorEventArgs ee = (ErrorEventArgs)e;
				var dispatcher = Mvx.Resolve<IMvxMainThreadDispatcher> ();
				dispatcher.RequestMainThreadAction (() => {
					AlertDialog.Builder builder = new AlertDialog.Builder (this);
					builder.SetTitle (ee.Title);
					builder.SetMessage (ee.Message);
					builder.SetNegativeButton (ee.CloseTitle, (EventHandler<DialogClickEventArgs>)null);
					builder.Show ();
				});
			};


			View BottomView = _view.FindViewById (Resource.Id.post_view_bottom_layout);
			web.BottomReached += (sender, e) => {
				if (IsBottomViewAnimating) return;
				if(BottomView.Visibility == ViewStates.Gone) {
					//show
					IsBottomViewAnimating = true;
					var al = new AnimatorListener();
					al.OnAnimationEndAction  = (Animator animation) => {
						IsBottomViewAnimating = false;
					};
					if (BottomView.TranslationY < _view.Height){
						//width of the view is zero before layout
						BottomView.TranslationY = _view.Height; //set to right
					}
					BottomView.Visibility = ViewStates.Visible;
					BottomView.Animate().TranslationY(0).SetListener(al).SetDuration(500);
				}
			};

			web.UnBottomReached += (sender, e) => {
				if (IsBottomViewAnimating) return;
				if(BottomView.Visibility == ViewStates.Visible){
					//hide
					IsBottomViewAnimating = true;
					var al = new AnimatorListener();
					al.OnAnimationEndAction  = (Animator animation) => {
						BottomView.Visibility = ViewStates.Gone;
						IsBottomViewAnimating = false;
					};
					BottomView.Animate().TranslationY(BottomView.Height).SetListener(al).SetDuration(500);
				} 
			};

			this.StartCalled += (sender, e) => PostViewModel.LoadPost();
			//this.OnBackPressed
		}

		public override void Finish ()
		{
			base.Finish ();
			CatalogNewsView.FromHome = false;
			this.OverridePendingTransition (Resource.Animation.slide_left_right, Resource.Animation.slide_right_right);
		}
		public override void OnBackPressed(){
			System.Diagnostics.Debug.WriteLine ("OnBackPressed");
			if (CommentsView.Visibility == ViewStates.Visible) {
				PostViewModel.ShowCommentCommand.Execute(null);

				HideSoftkeyboard ();
				return;
			}
			base.OnBackPressed ();
		}

		private void HideSoftkeyboard(){
			//hide softkeyboard
			InputMethodManager manager = (InputMethodManager) GetSystemService(InputMethodService);
			manager.HideSoftInputFromWindow(InputCommentEditText.WindowToken, 0);
		}

		public class AnimatorListener : Java.Lang.Object, Animator.IAnimatorListener
		{
			public Action<Animator> OnAnimationCancelAction, OnAnimationEndAction, OnAnimationRepeatAction, OnAnimationStartAction;
			public void OnAnimationCancel (Animator animation)
			{
				if (OnAnimationCancelAction != null) {
					OnAnimationCancelAction (animation);
				}
			}

			public void OnAnimationEnd (Animator animation)
			{
				if (OnAnimationEndAction != null) {
					OnAnimationEndAction (animation);
				}
			}

			public void OnAnimationRepeat (Animator animation)
			{
				if (OnAnimationRepeatAction != null) {
					OnAnimationRepeatAction (animation);
				}
			}

			public void OnAnimationStart (Animator animation)
			{
				if (OnAnimationStartAction != null) {
					OnAnimationStartAction (animation);
				}
			}
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.abar_post_menu, menu);       

			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				Finish();
				return true;
			}
			return base.OnOptionsItemSelected(item);
		}


		public class ObservableWebview : WebView {
			public event EventHandler BottomReached;
			public event EventHandler UnBottomReached;

			public ObservableWebview(Context context) : base(context)
			{
				
			}

			public ObservableWebview(Context context, IAttributeSet attrs) : base(context, attrs)
			{
			}

			public ObservableWebview(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
			{
			}

			protected override void OnScrollChanged (int left, int top, int oldLeft, int oldTop)
			{
				base.OnScrollChanged (left, top, oldLeft, oldTop);
				 {
					if ((this.ContentHeight * Resources.DisplayMetrics.Density - (top + this.Height)) <= 10) {
						if (BottomReached != null)
							BottomReached (this, EventArgs.Empty);
					} else {
						if (UnBottomReached != null)
							UnBottomReached (this, EventArgs.Empty);
					}
				}
			}
		}
	}
}

