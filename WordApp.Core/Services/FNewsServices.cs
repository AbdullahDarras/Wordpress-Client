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
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using ModernHttpClient;
using System.Net.Http.Headers;
using FSoft.WordApp.Core.Services.Services;

namespace FSoft.WordApp.Core.Services
{
	public class FNewsServices : IFNewsService
	{
		public FNewsServices ()
		{
		}

		private HttpRequestMessage Create(string uri, HttpMethod method)
		{
			Debug.WriteLineIf(Debugger.IsAttached, uri);

			HttpRequestMessage request = new HttpRequestMessage(method, new Uri(uri));

			request.Headers.Add("Accept", "application/json");
//			//request.Headers.Add ("http.protocol.single-cookie-header", "true");
//
//			foreach (string cookie in Settings.COOKIES_STR) {
//				Debug.WriteLineIf (Debugger.IsAttached, "CucumberServices Added Cookie " + cookie);
//			
//				//request.Headers.Add ("Set-Cookie", cookie);
//			}

			return request;
		}

		public Task<ResponseListCategory> GetListCategory(RequestListCategory request)
		{
			var response = CallService<ResponseListCategory, RequestListCategory>(Settings.GetListCategoryUrl, HttpMethod.Post, request);

			return response;
		}

		public Task<ResponseHomePosts> GetHomePosts (RequestHomePosts request)
		{
			var response = CallService<ResponseHomePosts, RequestHomePosts>(Settings.GetHomePostsUrl, HttpMethod.Post, request);

			return response;
		}

		public Task<ResponseListPost> GetRecentPosts(RequestRecentPosts request)
		{
			var response = CallService<ResponseListPost, RequestRecentPosts>(Settings.GetRecentPostsUrl, HttpMethod.Post, request);

			return response;
		}

		public Task<ResponseListPost> GetCategoryPosts(RequestCategoryPosts request)
		{
			var response = CallService<ResponseListPost, RequestCategoryPosts>(Settings.GetCategoryPostsUrl, HttpMethod.Post, request);

			return response;
		}

		public Task<ResponsePost> GetPost(RequestPost request)
		{
			var response = CallService<ResponsePost, RequestPost>(Settings.GetPostUrl, HttpMethod.Post, request);

			return response;
		}

		async public Task<ResponseAuthCookie> GenrateAuthCookie() {
			//get nonce
			ResponseNonce retNonce = await CallService<ResponseNonce, RequestNonce> (Settings.GetNonceAuthCookieUrl, HttpMethod.Get, new RequestNonce ());

			if (retNonce != null) {
				Settings.WP_AUTH_COOKIE_NONCE = retNonce.Nonce;
			Debug.WriteLine ("done gen aut cookie " + Settings.WP_AUTH_COOKIE_NONCE);

			//generate auth cookie
			ResponseAuthCookie retAuthCookie = await CallService<ResponseAuthCookie, RequestAuthCookie> (Settings.GenerateAuthCookieUrl, HttpMethod.Post, new RequestAuthCookie ());
			Settings.WP_AuthCookie = retAuthCookie;
			Debug.WriteLine ("WP Auth Cookie: {0} - {1} - {2}", retAuthCookie.User.Username, retAuthCookie.User.Displayname, retAuthCookie.Cookie);
			return Settings.WP_AuthCookie;
			} else {
				return null;
			}
		}

		async public Task<ResponseLike> LikePost(int postId) {
			var response = await CallService<ResponseLike, RequestLike> (Settings.WPULikeUrl, HttpMethod.Post, new RequestLike(postId));
			return response;
		}

		async public Task<ResponseLike> UnLikePost(int postId) {
			var response = await CallService<ResponseLike, RequestUnLikePost> (Settings.WPULikeUrl, HttpMethod.Post, new RequestUnLikePost(postId));
			return response;
		}

		async public Task<ResponseComments> GetComments (RequestComments request) {
			var response = await CallService<ResponseComments, RequestComments> (Settings.URL_GET_COMMENTS, HttpMethod.Post, request);
			return response;
		}

		async public Task<ResponsePostComment> PostComment(RequestPostComment request) {
			var response = await CallService<ResponsePostComment, RequestPostComment> (Settings.URL_POST_COMMENT, HttpMethod.Post, request);
			return response;
		}

		async public Task<ResponseUpdate> GetUpdate(RequestUpdate request) {
			var response = await CallService<ResponseUpdate, RequestUpdate> (Settings.URL_UPDATE, HttpMethod.Post, request);
			return response;
		}

		async private Task<T> Read<T>(HttpClient client, HttpRequestMessage request)
		{
			try
			{
				var response = await client.SendAsync(request);
				var jsonResponse = await response.Content.ReadAsStringAsync();

				if ( typeof(T) == typeof(ResponseLike) ){
					jsonResponse = "{ LikedCode:\"" + jsonResponse + "\"}";//string.Format("{LikedCode:}", jsonResponse);
				} else if ( typeof(T) == typeof(ResponsePostComment) ){
					return  await Task.Factory.StartNew(new Func<T>(() =>
						(T)Activator.CreateInstance (typeof(T), jsonResponse)));
				} else {
					
				}

				Debug.WriteLineIf(Debugger.IsAttached, jsonResponse);
				return JsonConvert.DeserializeObject<T>(jsonResponse);
			}
			catch (Exception e)
			{
				#if DEBUG
				Debug.WriteLine("M:" + e.ToString());
				#endif
				throw;
//				return default(T);
			}
		}

		async private Task<T> CallService<T>(string uri, HttpMethod method)
		{
//			var cookieContainer = new CookieContainer();
			//var cookieHandler = new NativeCookieHandler();
			using (var handler = new NativeMessageHandler(true,true, Settings.mNativeCookieHandler))
				
			using (HttpClient client = new HttpClient(handler))
			{
				AddCookies (Settings.URI, Settings.mNativeCookieHandler);

				using (HttpRequestMessage request = Create(uri, method))
				{
					return await Read<T>(client, request);
				}

			}
		}


		async private Task<TResponse> CallService<TResponse, TRequest>(string uri, HttpMethod method, TRequest data)
		{
//			var cookieContainer = new CookieContainer();
			//var cookieHandler = new NativeCookieHandler();
			AddCookies (Settings.URI, Settings.mNativeCookieHandler);
			using (var handler = new NativeMessageHandler(false,false, null))
				
			using (HttpClient client = new HttpClient(handler))
			{
				using (HttpRequestMessage request = Create(uri, method))
				{
					if (method != HttpMethod.Get)
					{
//						var jsonContent = JsonConvert.SerializeObject(data);
						request.Content = new StringContent(data.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

						#if DEBUG
						Debug.WriteLine("Req: ", data.ToString());
						#endif
					}

					return await Read<TResponse>(client, request);
				}
			}
		}

		private void AddCookies(Uri uri, NativeCookieHandler cookieHandler) {
			return;
			ICollection<Cookie> cc = Settings.COOKIES;
			Cookie[] array = new Cookie[cc.Count];
			cc.CopyTo(array, 0);
			cookieHandler.SetCookies (array);
//			Debug.WriteLineIf(Debugger.IsAttached, "START added Cookie");
//			foreach (Cookie cookie in cc) {
//				Debug.WriteLineIf(Debugger.IsAttached, "Added Cookie " + uri.AbsoluteUri +": " + cookie.Name + ": " + cookie.Value);
//
//				cookieHandler.Add(uri, new Cookie(cookie.Name, cookie.Value));
//			}
		}
	}
}

