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
using Cirrious.MvvmCross.Binding.Droid.Target;
using Android.Widget;
using Cirrious.MvvmCross.Binding;
using FSoft.WordApp.Droid;

namespace FSoft.WordApp.Droid.Binding
{
	public class UserAvatarShapeBackgroundBinding : MvxAndroidTargetBinding
	{
		private readonly RelativeLayout _Layout;
		public UserAvatarShapeBackgroundBinding(RelativeLayout view) : base(view)
		{
			this._Layout = view;
		}
		protected override void SetValueImpl(object target, object value)
		{
			// to do logic
		}

		public override void SetValue(object value)
		{
			var userId = (int)value;
			//System.Diagnostics.Debug.WriteLine ("Avatar Binding: " + userId);
			//userId = (new Random ()).Next (123456);
			if (userId % 5 == 0)
				_Layout.SetBackgroundResource(Resource.Drawable.circle_background);
			else if (userId % 5 == 1)
				_Layout.SetBackgroundResource(Resource.Drawable.circle_background_1);
			else if (userId % 5 == 2)
				_Layout.SetBackgroundResource(Resource.Drawable.circle_background_2); 
			else if (userId % 5 == 3)
				_Layout.SetBackgroundResource(Resource.Drawable.circle_background_3);
			else
				_Layout.SetBackgroundResource(Resource.Drawable.circle_background_4);
		}

		public override Type TargetType
		{
			get { return typeof(int); }
		}
		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.OneTime; }
		}        

	}
}

