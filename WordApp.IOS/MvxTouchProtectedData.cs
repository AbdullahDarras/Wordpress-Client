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


using Foundation;
using Security;


namespace Beezy.MvvmCross.Plugins.SecureStorage.Touch
{
    public class MvxTouchProtectedData : IMvxProtectedData
    {
        public void Protect(string key, string value)
        {
			Remove (key);

            var code = SecKeyChain.Add(new SecRecord(SecKind.GenericPassword)
            {
                Service = NSBundle.MainBundle.BundleIdentifier,
                Account = key,
				ValueData = NSData.FromString(value, NSStringEncoding.UTF8)
            });
        }

        public string Unprotect(string key)
        {
            var existingRecord = new SecRecord(SecKind.GenericPassword)
            {
                Account = key,
                Service = NSBundle.MainBundle.BundleIdentifier
            };

            // Locate the entry in the keychain, using the label, service and account information.
            // The result code will tell us the outcome of the operation.
            SecStatusCode resultCode;

			string str = null;
			NSData find = SecKeyChain.QueryAsData( existingRecord );
			if( find != null )
			{
				str = find.ToString();

			}
			return str;

        }

        public void Remove(string key)
        {
            var existingRecord = new SecRecord(SecKind.GenericPassword)
            {
                Account = key,
                Service = NSBundle.MainBundle.BundleIdentifier
            };
			var code = SecKeyChain.Remove(existingRecord);
        }
    }
}
