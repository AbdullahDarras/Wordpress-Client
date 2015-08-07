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


namespace HttpClient {
	
	
	// Base type probably should be MonoTouch.Foundation.NSObject or subclass
	[Foundation.Register("AppDelegate")]
	public partial class AppDelegate {
		
		private UIKit.UIWindow __mt_window;
		
		private UIKit.UIButton __mt_button1;
		
		private UIKit.UITableView __mt_stack;
		
		private UIKit.UINavigationController __mt_navigationController;
		
		#pragma warning disable 0169
		[Foundation.Connect("window")]
		private UIKit.UIWindow window {
			get {
				this.__mt_window = ((UIKit.UIWindow)(this.GetNativeField("window")));
				return this.__mt_window;
			}
			set {
				this.__mt_window = value;
				this.SetNativeField("window", value);
			}
		}
		
		[Foundation.Connect("button1")]
		private UIKit.UIButton button1 {
			get {
				this.__mt_button1 = ((UIKit.UIButton)(this.GetNativeField("button1")));
				return this.__mt_button1;
			}
			set {
				this.__mt_button1 = value;
				this.SetNativeField("button1", value);
			}
		}
		
		[Foundation.Connect("stack")]
		private UIKit.UITableView stack {
			get {
				this.__mt_stack = ((UIKit.UITableView)(this.GetNativeField("stack")));
				return this.__mt_stack;
			}
			set {
				this.__mt_stack = value;
				this.SetNativeField("stack", value);
			}
		}
		
		[Foundation.Connect("navigationController")]
		private UIKit.UINavigationController navigationController {
			get {
				this.__mt_navigationController = ((UIKit.UINavigationController)(this.GetNativeField("navigationController")));
				return this.__mt_navigationController;
			}
			set {
				this.__mt_navigationController = value;
				this.SetNativeField("navigationController", value);
			}
		}
	}
}
