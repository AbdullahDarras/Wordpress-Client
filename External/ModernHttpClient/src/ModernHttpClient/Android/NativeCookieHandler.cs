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

using System.Collections.Generic;
using System.Linq;
using System.Net;
using Java.Net;

namespace ModernHttpClient
{

    public class NativeCookieHandler
    {
        readonly CookieManager cookieManager = new CookieManager();

        public NativeCookieHandler()
        {
			cookieManager.SetCookiePolicy(CookiePolicy.AcceptAll);
            CookieHandler.Default = cookieManager; //set cookie manager if using NativeCookieHandler
        }

        public void SetCookies(IEnumerable<Cookie> cookies)
        {
            foreach (var nc in cookies.Select(ToNativeCookie)) {
                cookieManager.CookieStore.Add(new URI(nc.Domain), nc);
            }
        }
            
        public List<Cookie> Cookies {
            get {
                return cookieManager.CookieStore.Cookies
                    .Select(ToNetCookie)
                    .ToList();
            }
        }

        static HttpCookie ToNativeCookie(Cookie cookie)
        {
            var nc = new HttpCookie(cookie.Name, cookie.Value);
            nc.Domain = cookie.Domain;
            nc.Path = cookie.Path;
            nc.Secure = cookie.Secure;

            return nc;
        }

        static Cookie ToNetCookie(HttpCookie cookie)
        {
            var nc = new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
            nc.Secure = cookie.Secure;

            return nc;
        }
    }
}
