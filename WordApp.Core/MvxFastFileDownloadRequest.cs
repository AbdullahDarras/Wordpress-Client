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
using Cirrious.MvvmCross.Plugins.DownloadCache;
using Cirrious.CrossCore.Core;
using System.Net.Http;
using ModernHttpClient;
using System.Threading.Tasks;
using System.IO;

namespace FSoft.WordApp.Core
{
	public class MvxFastFileDownloadRequest
	{
		public MvxFastFileDownloadRequest(string url, string downloadPath)
		{
			Url = url;
			DownloadPath = downloadPath;
		}

		public string DownloadPath { get; private set; }
		public string Url { get; private set; }

		public event EventHandler<MvxFileDownloadedEventArgs> DownloadComplete;
		public event EventHandler<MvxValueEventArgs<Exception>> DownloadFailed;

		HttpClient CreateClient()
		{
			NativeCookieHandler cookieHandler = Settings.mNativeCookieHandler;//new NativeCookieHandler ();
			NativeMessageHandler handler = new NativeMessageHandler(true,true,cookieHandler){
				UseCookies = true,
			};
			return new HttpClient(handler);
		}

		public void Start()
		{
			var client = CreateClient();

			Task<HttpResponseMessage> result;

			result = client.GetAsync(Url);

			result.ContinueWith(res =>  {

				var httpResult = res.Result;
				httpResult.EnsureSuccessStatusCode(); 
				httpResult.Content.ReadAsStreamAsync()
					.ContinueWith(HandleSuccess, 
						TaskContinuationOptions.NotOnFaulted)
					.ContinueWith(ae => FireDownloadFailed(ae.Exception), 
						TaskContinuationOptions.OnlyOnFaulted);

			}).ContinueWith(ae => FireDownloadFailed(ae.Exception), TaskContinuationOptions.OnlyOnFaulted);

		}

		private void HandleSuccess(Task<Stream> result)
		{
			try
			{
				var fileService = MvxFileStoreHelper.SafeGetFileStore();
				var tempFilePath = DownloadPath + ".tmp";

				using (result.Result)
				{
					fileService.WriteFile(tempFilePath,
						(fileStream) =>
						{
							result.Result.CopyTo(fileStream);
						});
				}
				fileService.TryMove(tempFilePath, DownloadPath, true);
			}
			catch (Exception exception)
			{
				FireDownloadFailed(exception);
				return;
			}

			FireDownloadComplete();
		}

		private void FireDownloadFailed(Exception exception)
		{
			var handler = DownloadFailed;
			if (handler != null)
				handler(this, new MvxValueEventArgs<Exception>(exception));
		}

		private void FireDownloadComplete()
		{
			var handler = DownloadComplete;
			if (handler != null)
				handler(this, new MvxFileDownloadedEventArgs(Url, DownloadPath));
		}
	}
}
