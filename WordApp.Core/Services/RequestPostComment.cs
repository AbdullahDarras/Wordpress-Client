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

namespace FSoft.WordApp.Core.Services
{
	public class RequestPostComment : RequestBase
	{
		public int PostID;
		public string Content;
		public string Name;
		public string Email;
		public int UserId;

		public RequestPostComment (int post_id, string content)
		{
			PostID = post_id;
			Content = Uri.EscapeDataString(content);

			if (Settings.wpLoggedIn) {
				Name = Settings.wpUsername;
				Email = Settings.WP_AuthCookie.User.Email;
				UserId = Settings.WP_AuthCookie.User.Id;
			}
		}

		public RequestPostComment (int post_id, string content, string name, string email)
		{
			PostID = post_id;
			Content = Uri.EscapeDataString(content);
			Name = name;
			Email = email;
			UserId = -1;
		}

		public override string ToString ()
		{
			return string.Format ("comment_author={0}&comment_author_email={1}&post_id={2}&content={3}&user_id={4}", Name, Email, PostID, Content, UserId);
		}
	}
}

