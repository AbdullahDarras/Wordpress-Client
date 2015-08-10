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
using System.Net;
using System.Net.Http;
using FSoft.WordApp.Core.Services;
using System.Collections.Generic;
using ModernHttpClient;
using FSoft.WordApp.Core.Models;
using System.Collections.ObjectModel;
using FSoft.WordApp.Core.ViewModels;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;

namespace FSoft.WordApp.Core
{
	public class Settings
	{
		#if DEBUG
		public const string BaseUrl = "http://52.74.254.15/fsoftwordapp/";
		#else
		public const string BaseUrl = "http://52.74.254.15/fsoftwordapp/";
		#endif

		public const string APIUrl = BaseUrl + "?json=";
		public const string LoginUrl = BaseUrl + "wp-login.php"; /*arg: log=username&pwd=password */
		public const string ProfileUrl = BaseUrl + "wp-admin/profile.php";
		public const string GetListCategoryUrl = APIUrl + "core.get_category_index";
		public const string GetHomePostsUrl = APIUrl + "fnews.home_posts";
		public const string GetRecentPostsUrl = APIUrl + "core.get_recent_posts";
		public const string GetPostUrl = APIUrl + "core.get_post"; /* arg: post_id */
		public const string GetPageUrl = APIUrl + "core.get_page";/* arg: page_id */
		//respond controller submit comment is incorrect - user_id is alway zero :((
		public const string URL_GET_COMMENTS = APIUrl + "fnews.get_comments";
		public const string URL_POST_COMMENT = APIUrl + "fnews.post_comment";//"respond/submit_comment";/* arg: page_id */
		public const string URL_UPDATE = APIUrl + "fnews.check_update";
		public const string WPULikeUrl = BaseUrl + "wp-admin/admin-ajax.php";
		public const string GetCategoryPostsUrl = APIUrl + "core.get_category_posts";/* arg: category_id */
		public const string GetNonceAuthCookieUrl = APIUrl + "core.get_nonce&controller=auth&method=generate_auth_cookie";
		public const string GenerateAuthCookieUrl = APIUrl + "auth.generate_auth_cookie&";

		public static ICollection<Cookie> COOKIES = null;
		public static List<string> COOKIES_STR = new List<string> ();
		public static Uri URI = new Uri(BaseUrl);
		public static NativeCookieHandler mNativeCookieHandler = new NativeCookieHandler ();

		public static string DisplayName;
		public static Category CurrentCategory;
		public static int CurrentListPostPage; // current page of current category
		public static int CurrentListPostPages; // num of pages of current category
		public static ObservableCollection<CatalogPostsGroup> GroupedPosts;

		public const int RECENT_POST_CATEGORY_ID = -1;
		public const string RECENT_POST_CATEGORY_TITLE = "Home";


		public const string WP_KENTO_VOTE_UPVOTE_PARAMS = "action=kento_vote_insert&postid={0}&votetype=upvote";//post id
		public const string WP_KENTO_VOTE_UNVOTE_PARAMS = "action=kento_vote_delete&class_ul=upvoted&postid={0}&votetype=upvote";//post id
		public const string WP_KENTO_VOTE_RESULT_LIKED = "";
		public const string WP_KENTO_VOTE_RESULT_UNLIKED = "";

		public const string WP_KENTO_VOTE_LIKED_MSG = "Thank you! You liked this post.";
		public const string WP_KENTO_VOTE_DISLIKED_MSG = "Sorry! You disliked this post.";

		public const int WP_SLIDEMAIN_ID = 86;

		public const string WP_USERNAME_FIELD = "log";
		public const string WP_PWD_FIELD = "pwd";
		public const bool WP_NEED_LOGIN = false;
		public static bool wpLoggedIn = false;
		public static string wpUsername = "";
		public static string wpPassword = "";

		public static bool WP_USED_FEATURED_IMAGE = true;

		public static string WP_AUTH_COOKIE_NONCE = "";
		public static ResponseAuthCookie WP_AuthCookie = null;
		public static ResponseUpdate UpdateInfo = null;


		public const string MSG_INVALID_USERNAME_OR_PWD = "Invalid Username or Password";
		public const string MSG_EMPTY_USERNAME_OR_PWD = "Please fill Username and Password";
		public const string MSG_NETWORK_COMMON = "Please contact Admin or check your network";
		public const string MSG_NETWORK_NOT_REACHABLE = "No network available. Please check your network!";
		public const string MSG_COMMENT_EMPTY_TEXT_ERROR = "Please input your comment!";

		public const bool CHECK_UPDATE = true;
		public static bool UseTreeMenu = true;

		public const int MaxPostPossibleHeight = 350;
		public const int MinPostPossibleHeight = 200;

		public const int HOME_REFRESH_TIME = 30 * 3600 * 1000;

		public const bool USE_RECENT_POSTS = true;

		//cached data
		public static List<Category> Categories { get; set; } 
		public static List<Post> RecentPosts { get; set; }

		public static int COLOR_HIGHLIGHT = 0x263290;
		public const int PADDING_IOS = 16;
		public static DeviceInfo DeviceInfo;

		public Settings ()
		{
		}
	}
}

