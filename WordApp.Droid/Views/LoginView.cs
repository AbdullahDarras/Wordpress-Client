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

using FSoft.WordApp.Core.ViewModels;
using Cirrious.CrossCore.Platform;
using Android.App;
using Android.Graphics;
using Android.Views.InputMethods;
using Cirrious.CrossCore.Core;
using Cirrious.CrossCore;
using System;
using Android.Content;

namespace FSoft.WordApp.Droid
{
	[Activity (Label = "Login", MainLauncher = false, Theme = "@style/Theme.AppCompat.Light.NoActionBar", WindowSoftInputMode=SoftInput.AdjustResize)]

	public class LoginView : MvxActionBarActivity, ViewTreeObserver.IOnGlobalLayoutListener
	{
		private LoginViewModel LoginViewModel {get { return base.ViewModel as LoginViewModel;}}
		private View LoginRootView;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			LoginRootView = this.BindingInflate(Resource.Layout.LoginView, null);

			SetContentView (LoginRootView);

			LoginRootView.ViewTreeObserver.AddOnGlobalLayoutListener (this);
			((LoginViewModel)base.ViewModel).PropertyChanged += (sender, e) => {
				if (e.PropertyName == LoginViewModel.IsLoadingPropertyName)
				if (LoginViewModel.IsLoading){
					InputMethodManager manager = (InputMethodManager) GetSystemService(InputMethodService);
					manager.HideSoftInputFromWindow(LoginRootView.WindowToken, 0);
				}
			};

			LoginViewModel.ErrorHandler += (sender, e) => {
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

			LoginViewModel.UpdateClientHandler += (sender, e) => {
				var ee = (UpdateClientEventArgs)e;
				var dispatcher = Mvx.Resolve<IMvxMainThreadDispatcher> ();
				dispatcher.RequestMainThreadAction (() => {
					AlertDialog.Builder builder = new AlertDialog.Builder (this);
					builder.SetTitle (ee.Title);
					builder.SetMessage (ee.Message);
					builder.SetNegativeButton (ee.CloseTitle, new EventHandler<DialogClickEventArgs>(
						(s, args) => {
							LoginViewModel.showUpdateStore();
							Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
						}));
					builder.Show ();
				});
			};

			((LoginViewModel)base.ViewModel).IsLoading = false;

			RootView.FromLogin = true;
		}

		void ViewTreeObserver.IOnGlobalLayoutListener.OnGlobalLayout ()
		{
			Rect r = new Rect();
			//r will be populated with the coordinates of your view that area still visible.
			LoginRootView.GetWindowVisibleDisplayFrame (r);
			int heightDiff = LoginRootView.RootView.Height - (r.Bottom - r.Top);
			System.Diagnostics.Debug.WriteLine ("OnGlobalLayout: {0} - {1} - {2} ", LoginRootView.Height, LoginRootView.RootView.Height, heightDiff );
			if (heightDiff > 201) { // if more than 100 pixels, its probably a keyboard...
				if (!LoginViewModel.IsSoftkeyShowed){
					System.Diagnostics.Debug.WriteLine ("Softkey show");
					LoginViewModel.IsSoftkeyShowed = true;
					LoginViewModel.IsSoftkeyHided = false;
				}
			} else {
				if (!LoginViewModel.IsSoftkeyHided) {
					System.Diagnostics.Debug.WriteLine ("Softkey hide");
					LoginViewModel.IsSoftkeyShowed = false;
					LoginViewModel.IsSoftkeyHided = true;
				}
			}
		}
	}
}

