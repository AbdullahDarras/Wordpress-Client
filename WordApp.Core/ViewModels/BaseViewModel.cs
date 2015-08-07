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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using System.Linq.Expressions;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;
using FSoft.WordApp.Core.Services;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.WebBrowser;

namespace FSoft.WordApp.Core.ViewModels
{

	public class BaseViewModel : MvxViewModel
    {
		public event EventHandler ErrorHandler;
		private readonly IMvxDeviceInfo _deviceInfoPlugin;
		private NetworkStatus _status;

		protected long LastTimeLoadedData = -1;

		public const string IsLoadingPropertyName = "IsLoading";
		private bool isLoading = false;
		public virtual bool IsLoading {
			get { return isLoading; }
			set { 
				SetProperty (ref isLoading, value, IsLoadingPropertyName);
			}
		}

		protected IFNewsService Service;

		public override void Start()
		{
			base.Start();

			DeviceInfo = _deviceInfoPlugin.GetDeviceInfo();
			Status = _deviceInfoPlugin.NetworkStatus;
		}

		public BaseViewModel(IFNewsService service, IMvxDeviceInfo deviceInfoPlugin)
		{
			Service = service;

			_deviceInfoPlugin = deviceInfoPlugin;
			DeviceInfo = _deviceInfoPlugin.GetDeviceInfo();
			Settings.DeviceInfo = DeviceInfo;
			Status = _deviceInfoPlugin.NetworkStatus;
		}

		private DeviceInfo _deviceInfo;
		public DeviceInfo DeviceInfo
		{
			get { return _deviceInfo; }
			set { _deviceInfo = value; RaisePropertyChanged(() => DeviceInfo); }
		}

		public NetworkStatus Status
		{
			get { return _status; }
			private set { _status = value; RaisePropertyChanged(() => Status); }
		}

		public void ShowErrorMessage(string msg, Exception e = null) {
			#if DEBUG
			System.Diagnostics.Debug.WriteLine("BVM: " +msg + "\t" + (e == null?"":e.Message));
			if (ErrorHandler != null) {
				ErrorHandler (this, new ErrorEventArgs("Error", msg + (e == null?"":e.Message), "CLOSE"));
			}
			#else
			if (ErrorHandler != null) {
				ErrorHandler (this, new ErrorEventArgs("Error", msg, "CLOSE"));
			}
			#endif
		}

		public virtual void RefreshData() {

		}

		protected void ShowWebPage(string webPage)
		{
			var task = Mvx.Resolve<IMvxWebBrowserTask>();
			task.ShowWebPage(webPage);
		}
	}

	public class ErrorEventArgs : EventArgs
	{
		public string Title { get; private set; }
		public string Message { get; private set; }
		public string CloseTitle { get; private set;}
		public ErrorEventArgs(string title, string message, string closeTitle)
		{
			Title = title;
			Message = message;
			CloseTitle = closeTitle;
		}
	}
}
