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
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using System.Net.Http;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using ModernHttpClient;

namespace HttpClient
{
	public class Application
	{
		// URL where we fetch the wisdom from
		public const string WisdomUrl = "http://httpbin.org/ip";

		static void Main (string[] args)
		{
			UIApplication.Main (args);
		}

		public static void Busy ()
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}
		
		public static void Done ()
		{
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;	
		}
			
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window.AddSubview (navigationController.View);
			
			button1.TouchDown += Button1TouchDown;
			TableViewSelector.Configure (this.stack, new string [] {
				"WebRequest",
				"HttpClient/CFNetwork",
				"HttpClient/NSURLSession"
			});
			                   
			window.MakeKeyAndVisible ();
			
			return true;
		}

		async void Button1TouchDown (object sender, EventArgs e)
		{
			// Do not queue more than one request
			if (UIApplication.SharedApplication.NetworkActivityIndicatorVisible)
				return;
			
			switch (stack.SelectedRow ()){
			case 0:
				new DotNet (this).HttpSample ();
				break;
			case 1:
				await new NetHttp (this).HttpSample (new CFNetworkHandler ());
				break;
			case 2:
				await new NetHttp (this).HttpSample (new NativeMessageHandler());
				break;
			}
		}

		public void RenderStream (Stream stream)
		{
			var reader = new System.IO.StreamReader (stream);

			InvokeOnMainThread (delegate {
				var view = new UIViewController ();
				view.View.BackgroundColor = UIColor.White;
				var label = new UILabel (new CGRect (20, 60, 300, 80)){
					Text = "The HTML returned by the server:"
				};
				var tv = new UITextView (new CGRect (20, 140, 300, 400)){
					Text = reader.ReadToEnd ()
				};
				view.Add (label);
				view.Add (tv);
					
				navigationController.PushViewController (view, true);
			});			
		}
		
		// This method is required in iPhoneOS 3.0
		public override void OnActivated (UIApplication application)
		{

		}
	}
}
