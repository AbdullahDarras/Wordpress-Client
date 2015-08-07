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

/* 
 * Copyright 2012-2014 Jeremy Feinstein
 * Copyright 2013-2014 Tomasz Cielecki
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using Android.OS;
using Android.Preferences;
using Android.Views;

namespace SlidingMenuSharp.App
{
    public class SlidingPrefereceActivity : PreferenceActivity, ISlidingActivity
    {
        private SlidingActivityHelper _helper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _helper = new SlidingActivityHelper(this);
            _helper.OnCreate(savedInstanceState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            _helper.OnPostCreate(savedInstanceState);
        }

        public override View FindViewById(int id)
        {
            var v = base.FindViewById(id);
            return v ?? _helper.FindViewById(id);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            _helper.OnSaveInstanceState(outState);
        }

        public override void SetContentView(int layoutResId)
        {
            SetContentView(LayoutInflater.Inflate(layoutResId, null));
        }

        public override void SetContentView(View view)
        {
            SetContentView(view, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent));
        }

        public override void SetContentView(View view, ViewGroup.LayoutParams @params)
        {
            base.SetContentView(view, @params);
            _helper.RegisterAboveContentView(view, @params);
        }

        public void SetBehindContentView(View view, ViewGroup.LayoutParams layoutParams)
        {
            _helper.SetBehindContentView(view, layoutParams);
        }

        public void SetBehindContentView(View view)
        {
            SetBehindContentView(view, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent));
        }

        public void SetBehindContentView(int layoutResId)
        {
            SetBehindContentView(LayoutInflater.Inflate(layoutResId, null));
        }

        public SlidingMenu SlidingMenu
        {
            get { return _helper.SlidingMenu; }
        }

        public void Toggle()
        {
            _helper.Toggle();
        }

        public void ShowContent()
        {
            _helper.ShowContent();
        }

        public void ShowMenu()
        {
            _helper.ShowMenu();
        }

        public void ShowSecondaryMenu()
        {
            _helper.ShowSecondaryMenu();
        }

        public void SetSlidingActionBarEnabled(bool enabled)
        {
            _helper.SlidingActionBarEnabled = enabled;
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            var b = _helper.OnKeyUp(keyCode, e);
            return b ? b : base.OnKeyUp(keyCode, e);
        }
    }
}
