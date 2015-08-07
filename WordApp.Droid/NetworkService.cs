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
using Android.Net;
using Android.App;
using Android.Content;
using FSoft.WordApp.Core.Services;

namespace FSoft.WordApp.Droid.Services
{
	public class NetworkService : INetworkService
	{
		ConnectivityManager connectivityManager;

        public bool IsReachable()
        {
            return true;
        }


		public NetworkService ()
		{
			connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Application.ConnectivityService);
		}

		#region INetworkService implementation

		public bool IsWifiConnected ()
		{
			var mobileState = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi).GetState();

			return mobileState == NetworkInfo.State.Connected;
		}

		public bool IsDataConnected ()
		{
			var mobileState = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile).GetState();

			return mobileState == NetworkInfo.State.Connected;
		}

		public bool TryReach (string uri)
		{
			throw new NotImplementedException ();
		}

		#endregion

	}
}

