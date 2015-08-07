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
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;

namespace ModernHttpClient
{
    public class NativeMessageHandler : HttpClientHandler
    {
        const string wrongVersion = "You're referencing the Portable version in your App - you need to reference the platform (iOS/Android) version";

        public bool DisableCaching { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ModernHttpClient.Portable.NativeMessageHandler"/> class.
        /// </summary>
        public NativeMessageHandler(): base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="ModernHttpClient.Portable.NativeMessageHandler"/> class.
        /// </summary>
        /// <param name="throwOnCaptiveNetwork">If set to <c>true</c> throw on
        /// captive network (ie: a captive network is usually a wifi network
        /// where an authentication html form is shown instead of the real
        /// content).</param>
        /// <param name="customSSLVerification">Enable custom SSL certificate 
        /// verification via ServicePointManager. Disabled by default for 
        /// performance reasons (i.e. the OS default certificate verification 
        /// will take place)</param>
        /// <param name="cookieHandler">Enable native cookie handling.
        /// </param>
        public NativeMessageHandler(bool throwOnCaptiveNetwork, bool customSSLVerification, NativeCookieHandler cookieHandler = null) : base()
        {
        }

        public void RegisterForProgress(HttpRequestMessage request, ProgressDelegate callback)
        {
            throw new Exception(wrongVersion);
        }
    }

    public class ProgressStreamContent : StreamContent 
    {
        const string wrongVersion = "You're referencing the Portable version in your App - you need to reference the platform (iOS/Android) version";

        ProgressStreamContent(Stream stream) : base(stream)
        {
            throw new Exception(wrongVersion);
        }

        ProgressStreamContent(Stream stream, int bufferSize) : base(stream, bufferSize)
        {
            throw new Exception(wrongVersion);
        }

        public ProgressDelegate Progress {
            get { throw new Exception(wrongVersion); }
            set { throw new Exception(wrongVersion); }
        }
    }

    public delegate void ProgressDelegate(long bytes, long totalBytes, long totalBytesExpected);

    public class NativeCookieHandler
    {
        const string wrongVersion = "You're referencing the Portable version in your App - you need to reference the platform (iOS/Android) version";

        public void SetCookies(IEnumerable<Cookie> cookies)
        {
            throw new Exception(wrongVersion);
        }

        public List<Cookie> Cookies {
            get { throw new Exception(wrongVersion); }
        }
    }
}
