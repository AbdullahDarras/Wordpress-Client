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

namespace FSoft.WordApp.Core.Models
{
	public class ImageWP
	{
		public string Url { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}

	public class Images
	{
		public ImageWP Full { get; set; }
		public ImageWP Thumbnail { get; set; }
		public ImageWP Medium { get; set; }
		public ImageWP Large { get; set; }
	}

	public class Attachment
	{
		public int Id { get; set; }
		private string _Url;
		public string Url { 
			get { 
				return _Url;
			} 
			set {
				_Url = value;

				_Url = _Url.Replace ("techinsight.web.fsoft.com.vn", "techinsight.fsoft.com.vn");
				_Url = _Url.Replace ("cuder.fsoft.com.vn", "techinsight.fsoft.com.vn");
				_Url = _Url.Replace ("web.fsoft.com.vn", "techinsight.fsoft.com.vn");
				_Url = _Url.Replace ("techinsight.web.fsoft.com.vn", "techinsight.fsoft.com.vn");
				_Url = _Url.Replace ("http://techinsight.fsoft.com.vn", "https://techinsight.fsoft.com.vn");
			}
		}
		public string Slug { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Caption { get; set; }
		public int Parent { get; set; }
		public string Mime_type { get; set; }
		//public Images Images { get; set; }
	}
}

