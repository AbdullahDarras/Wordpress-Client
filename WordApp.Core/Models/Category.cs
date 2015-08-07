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
	/*
	"id": 31,
	"slug": "cong-nghe",
	"title": "Công Nghệ",
	"description": "",
	"parent": 0,
	"post_count": 7
	*/
	public class Category
	{
		public int Id { get; set;}
		public string Slug { get; set;}
		private string _Title;
		public string Title {
			get { return _Title;}
			set { 
				_Title = value;
				if (!string.IsNullOrEmpty (value)) {
					TitleUpper = value.ToUpper ();
				}
			}
		}
		public string Description { get; set; }
		public int Visible { get; set;} //1: visible
		public int Parent {get; set; }
		public int Post_Count {get; set; }
		public int Breaking_news { get; set; }

		public string TitleUpper { get; set;}
		public override string ToString ()
		{
			return string.Format ("[Category: Id={0}, Slug={1}, Title={2}, Description={3}, Parent={4}, PostCount={5}]", Id, Slug, Title, Description, Parent, Post_Count);
		}


		//for testing
		public Category(int id, string title){
			Id = id;
			Title = title;
		}
	}
}

