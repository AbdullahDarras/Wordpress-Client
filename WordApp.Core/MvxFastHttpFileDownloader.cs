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
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Plugins.DownloadCache;
using System.Collections.Generic;
using System.Linq;

namespace FSoft.WordApp.Core
{
	public class MvxFastHttpFileDownloader 
		: MvxLockableObject
	, IMvxHttpFileDownloader
	{
		private readonly Dictionary<MvxFastFileDownloadRequest, bool> _currentRequests =
			new Dictionary<MvxFastFileDownloadRequest, bool>();

		private const int DefaultMaxConcurrentDownloads = 30;
		private readonly int _maxConcurrentDownloads;
		private readonly Queue<MvxFastFileDownloadRequest> _queuedRequests = new Queue<MvxFastFileDownloadRequest>();

		public MvxFastHttpFileDownloader(int maxConcurrentDownloads = DefaultMaxConcurrentDownloads)
		{
			_maxConcurrentDownloads = maxConcurrentDownloads;
		}

		public void RequestDownload(string url, string downloadPath, Action success, Action<Exception> error)
		{
			var request = new MvxFastFileDownloadRequest(url, downloadPath);
			request.DownloadComplete += (sender, args) =>
			{
				OnRequestFinished(request);
				success();
			};
			request.DownloadFailed += (sender, args) =>
			{
				OnRequestFinished(request);
				error(args.Value);
			};

			RunSyncOrAsyncWithLock( () =>
				{
					_queuedRequests.Enqueue(request);
					if (_currentRequests.Count < _maxConcurrentDownloads)
					{
						StartNextQueuedItem();
					}
				});
		}

		private void OnRequestFinished(MvxFastFileDownloadRequest request)
		{
			RunSyncOrAsyncWithLock(() =>
				{
					_currentRequests.Remove(request);
					if (_queuedRequests.Any())
					{
						StartNextQueuedItem();
					}
				});
		}

		private void StartNextQueuedItem()
		{
			if (_currentRequests.Count >= _maxConcurrentDownloads)
				return;

			RunSyncOrAsyncWithLock(() =>
				{
					if (_currentRequests.Count >= _maxConcurrentDownloads)
						return;

					if (!_queuedRequests.Any())
						return;

					var request = _queuedRequests.Dequeue();
					_currentRequests.Add(request, true);
					request.Start();
				});
		}
	}
}
