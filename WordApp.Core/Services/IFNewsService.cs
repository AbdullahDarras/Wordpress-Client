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
using System.Threading.Tasks;

namespace FSoft.WordApp.Core.Services
{
	public interface IFNewsService
	{
		Task<ResponseListCategory> GetListCategory (RequestListCategory request);
		Task<ResponseListPost> GetRecentPosts (RequestRecentPosts request);
		Task<ResponseHomePosts> GetHomePosts (RequestHomePosts request);
		Task<ResponseListPost> GetCategoryPosts (RequestCategoryPosts request);
		Task<ResponsePost> GetPost (RequestPost request);
		Task<ResponseComments> GetComments (RequestComments request);
		Task<ResponseAuthCookie> GenrateAuthCookie ();
		Task<ResponseLike> LikePost (int postId);
		Task<ResponseLike> UnLikePost (int postId);
		Task<ResponsePostComment> PostComment (RequestPostComment request);
		Task<ResponseUpdate> GetUpdate(RequestUpdate request);

	}
}

