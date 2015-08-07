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
using System.Net;
using Foundation;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace HttpClient
{
	public class DotNet {
		AppDelegate ad;
		
		public DotNet (AppDelegate ad)
		{
			this.ad = ad;
		}
		
		//
		// Asynchronous HTTP request
		//
		public void HttpSample ()
		{
			Application.Busy ();
			var request = WebRequest.Create (Application.WisdomUrl);
			request.BeginGetResponse (FeedDownloaded, request);
		}
		
		//
		// Invoked when we get the stream back from the twitter feed
		// We parse the RSS feed and push the data into a 
		// table.
		//
		void FeedDownloaded (IAsyncResult result)
		{
			Application.Done ();
			var request = result.AsyncState as HttpWebRequest;
			
			try {
				var response = request.EndGetResponse (result);
				ad.RenderStream (response.GetResponseStream ());
			} catch (Exception e) {
				Debug.WriteLine (e);
			}
		}
	}
}
