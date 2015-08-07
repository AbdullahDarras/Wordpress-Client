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
using FSoft.WordApp.Core.Services;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;

namespace FSoft.WordApp.Core.ViewModels
{
	public class RecentPostsViewModel : CatalogNewsViewModel
	{
		public event EventHandler MenuButtonSelected; //top left menu button

		private Cirrious.MvvmCross.ViewModels.MvxCommand _MenuButtonSelectedCommand;
		public System.Windows.Input.ICommand MenuSelectedCommand
		{
			get
			{
				_MenuButtonSelectedCommand = _MenuButtonSelectedCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>{
					if (MenuButtonSelected != null){
						MenuButtonSelected(this,EventArgs.Empty);
					}
				});
				return _MenuButtonSelectedCommand;
			}
		}

		public RecentPostsViewModel (IFNewsService service, IMvxDeviceInfo deviceInfoPlugin): base(service, deviceInfoPlugin)
		{
			
		}
	}
}

