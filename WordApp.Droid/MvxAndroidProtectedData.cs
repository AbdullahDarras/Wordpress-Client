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
using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Preferences;
using Java.Security;

namespace Beezy.MvvmCross.Plugins.SecureStorage.Droid
{
    public class MvxAndroidProtectedData : IMvxProtectedData
    {
        private readonly ISharedPreferences _preferences;

        public MvxAndroidProtectedData()
        {
            _preferences = Application.Context.GetSharedPreferences(Application.Context.PackageName + ".SecureStorage",
                FileCreationMode.Private);
        }

        public void Protect(string key, string value)
        {
            var editor = _preferences.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }

        public string Unprotect(string key)
        {
            try
            {
                return _preferences.GetString(key, null);
            }
            catch
            {
                return null;
            }
        }

        public void Remove(string key)
        {
            if (_preferences.Contains(key))
            {
                var editor = _preferences.Edit();
                editor.Remove(key);
                editor.Commit();
            }
        }
    }
}
