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

namespace FSoft.WordApp.Core.Models {
	public class Comment {
		public int Id {
			get;
			set;
		}
		private string _Name;
		public string Name {
			get { return _Name;}
			set { 
				_Name = value; 
				if (!string.IsNullOrEmpty(_Name))
					UserCapitalize = _Name.ToUpper () [0] + "";
			}
		}
		public string Url {
			get;
			set;
		}
		public string Date {
			get;
			set;
		}
		public string Content {
			get;
			set;
		}
		public int Parent {
			get;
			set;
		}
		private Author _Author;
		public Author Author {
			get {
				return _Author;
			}
			set {
				_Author = value;
				if (_Author != null) {
					UserId = _Author.id;
					if (_Author == null) {

						if (!string.IsNullOrEmpty (_Author.slug)) {
							UserCapitalize = _Author.slug.ToUpper () [0] + "";
						} else {
							UserCapitalize = _Author.name.ToUpper () [0] + "";
						}
					} else {
						UserCapitalize = string.IsNullOrEmpty(Name) ? "" : Name.ToUpper () [0] + "";
					}
				}
			}
		}
		public int UserId {
			get;
			set;
		}
		//first charater of user name
		public string UserCapitalize {
			get;
			set;
		}
	}
}
