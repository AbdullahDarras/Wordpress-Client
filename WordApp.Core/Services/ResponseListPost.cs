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
using FSoft.WordApp.Core.Models;

namespace FSoft.WordApp.Core.Services
{
	public class ResponseListPost : ResponseBase
	{
		public string Status { get; set; }
		public int Count { get; set; }
		public int Count_total { get; set; }
		public int Pages { get; set; }
		public List<Post> Posts { get; set; }

//		public override string ToString ()
//		{
//			#if DEBUG
//			var pa = Posts.ToArray();
//			for (int i = 0; i< pa.Length; i++)
//				Debug.WriteLine(pa[i].ToString());
//			#endif
//			return string.Format ("[ResponseListRecentPosts: Status={0}, Count={1}, Count_total={2}, Pages={3}, Posts={4}]", Status, Count, Count_total, Pages, Posts.ToArray().Length);
//		}
	}
}

