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
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Bindings;
using Foundation;
using CoreGraphics;
using iOSUILib;

namespace FSoft.WordApp.IOS.Views {
	
	public class MvxMDTableViewCell: MDTableViewCell/*UITableViewCell*/,	IMvxBindable 
	{
		public IMvxBindingContext BindingContext {
			get;
			set;
		}

		public MvxMDTableViewCell(): this(string.Empty) {}

		public MvxMDTableViewCell(string bindingText) {
			this.CreateBindingContext(bindingText);
		}

		public MvxMDTableViewCell(IEnumerable < MvxBindingDescription > bindingDescriptions) {
			this.CreateBindingContext(bindingDescriptions);
		}

//		public MvxMDTableViewCell(string bindingText, CGRect frame): base(frame) {
//			this.CreateBindingContext(bindingText);
//		}

//		public MvxMDTableViewCell(IEnumerable < MvxBindingDescription > bindingDescriptions, CGRect frame): base(frame) {
//			this.CreateBindingContext(bindingDescriptions);
//		}

		public MvxMDTableViewCell(IntPtr handle): this(string.Empty, handle) {
			this.RippleColor = UIColor.White.ColorRGBAFromHex (0xEEEEEE11);
		}

		public MvxMDTableViewCell(string bindingText, IntPtr handle): base(handle) {
			this.CreateBindingContext(bindingText);
		}

		public MvxMDTableViewCell(IEnumerable < MvxBindingDescription > bindingDescriptions, IntPtr handle): base(handle) {
			this.CreateBindingContext(bindingDescriptions);
		}

//		public MvxMDTableViewCell(string bindingText, UITableViewCellStyle cellStyle, NSString cellIdentifier,
//			UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None): base(cellStyle, cellIdentifier) {
//			Accessory = tableViewCellAccessory;
//			this.CreateBindingContext(bindingText);
//		}

//		public MvxMDTableViewCell(IEnumerable < MvxBindingDescription > bindingDescriptions,
//			UITableViewCellStyle cellStyle, NSString cellIdentifier,
//			UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None): base(cellStyle, cellIdentifier) {
//			// note that we allow the virtual Accessory property to be set here - but do not seal
//			// it. Previous `sealed` code caused odd, unexplained behaviour in MonoTouch
//			// - see https://github.com/MvvmCross/MvvmCross/issues/524
//			Accessory = tableViewCellAccessory;
//			this.CreateBindingContext(bindingDescriptions);
//		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				BindingContext.ClearAllBindings();
			}
			base.Dispose(disposing);
		}

		public object DataContext {
			get {
				return BindingContext.DataContext;
			}
			set {
				BindingContext.DataContext = value;
			}
		}
	}
}
