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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FSoft.WordApp.Core.Models;

namespace FSoft.WordApp.Core.Services
{
	/*
status": "ok",
"count": 5,
"categories": [
{"id": 31,
"slug": "cong-nghe",
"title": "Công Nghệ",
"description": "",
"parent": 0,
"post_count": 7},
{"id": 33,
"slug": "nguoi-cuder",
"title": "Người Cuder",
"description": "",
"parent": 0,
"post_count": 7},
{"id": 30,
"slug": "su-kien-trong-ngay",
"title": "Sự Kiện Trong Ngày",
"description": "",
"parent": 0,
"post_count": 5},
{"id": 32,
"slug": "van-hoa-the-thao",
"title": "Văn Hóa - Thể Thao",
"description": "",
"parent": 0,
"post_count": 5},
{"id": 34,
"slug": "vom-me-xanh",
"title": "Vòm Me Xanh",
"description": "",
"parent": 0,
"post_count": 6}]
	*/
	public class ResponseListCategory : ResponseBase
	{
		public int Count {get; set;}
		public List<Category> Categories { get; set; } 

		public override string ToString ()
		{
			var cats = Categories.ToArray ();
			for (int i = 0; i < cats.Length; i++) {
				#if DEBUG
				Debug.WriteLine(cats[i].ToString());
				#endif
			
			}
			return string.Format ("[ResponseListCategory: Count={0}, Categories={1}]", Count, Categories.ToArray().Length);
		}
	}
}

