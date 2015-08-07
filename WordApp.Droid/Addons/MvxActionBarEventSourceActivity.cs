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

using System;
using Android.App;
using Android.Content;
using Android.OS;
using Cirrious.CrossCore.Droid.Views;
using Android.Support.V7.App;
using Cirrious.CrossCore.Core;

namespace FSoft.WordApp.Droid
{
	public class MvxActionBarEventSourceActivity : ActionBarActivity
	, IMvxEventSourceActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			CreateWillBeCalled.Raise(this, bundle);
			base.OnCreate(bundle);
			CreateCalled.Raise(this, bundle);
		}

		protected override void OnDestroy()
		{
			DestroyCalled.Raise(this);
			base.OnDestroy();
		}

		protected override void OnNewIntent(Intent intent)
		{
			base.OnNewIntent(intent);
			NewIntentCalled.Raise(this, intent);
		}

		protected override void OnResume()
		{
			base.OnResume();
			ResumeCalled.Raise(this);
		}

		protected override void OnPause()
		{
			PauseCalled.Raise(this);
			base.OnPause();
		}

		protected override void OnStart()
		{
			base.OnStart();
			StartCalled.Raise(this);
		}

		protected override void OnRestart()
		{
			base.OnRestart();
			RestartCalled.Raise(this);
		}

		protected override void OnStop()
		{
			StopCalled.Raise(this);
			base.OnStop();
		}

		public override void StartActivityForResult(Intent intent, int requestCode)
		{
			StartActivityForResultCalled.Raise(this, new MvxStartActivityForResultParameters(intent, requestCode));
			base.StartActivityForResult(intent, requestCode);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			ActivityResultCalled.Raise(this, new MvxActivityResultParameters(requestCode, resultCode, data));
			base.OnActivityResult(requestCode, resultCode, data);
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			SaveInstanceStateCalled.Raise(this, outState);
			base.OnSaveInstanceState(outState);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				DisposeCalled.Raise(this);
			}
			base.Dispose(disposing);
		}

		public event EventHandler DisposeCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> CreateWillBeCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> CreateCalled;
		public event EventHandler DestroyCalled;
		public event EventHandler<MvxValueEventArgs<Intent>> NewIntentCalled;
		public event EventHandler ResumeCalled;
		public event EventHandler PauseCalled;
		public event EventHandler StartCalled;
		public event EventHandler RestartCalled;
		public event EventHandler StopCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> SaveInstanceStateCalled;
		public event EventHandler<MvxValueEventArgs<MvxStartActivityForResultParameters>> StartActivityForResultCalled;
		public event EventHandler<MvxValueEventArgs<MvxActivityResultParameters>> ActivityResultCalled;
	}
}

