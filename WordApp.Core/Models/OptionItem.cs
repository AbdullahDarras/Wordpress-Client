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

using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FSoft.WordApp.Core.Models
{
	public enum OptionItemType {
		Category = 0,
		Signout = 1,
	}

	public class SignoutOptionItem: OptionItem {
		public override string Title { get { return "Sign out";}}

		public SignoutOptionItem() {
			Type = OptionItemType.Signout;
		}
	}

    public class CategoryOptionItem : OptionItem
    {
		public Category Category {get; set;}
		public CategoryOptionItem(Category cat){
			Category = cat;
			Type = OptionItemType.Category;
			Id = cat.Id;
		}

		public override string Title { get { return Category.Title; } }
		public override int Count { get { return Category.Post_Count; } }
		public override int Parent { get{ return Category.Parent;} }
    }

	public  abstract class OptionItem
    {
		public OptionItemType Type { get; set;}
		public int Id { get; set; }
        public virtual string Title { get { var n = GetType().Name; return n.Substring(0, n.Length - 10); } }
		public virtual int Parent { get; set; }
        public virtual int Count { get; set; }
        public virtual bool Selected { get; set; }
        public virtual string Icon { get { return 
                Title.ToLower().TrimEnd('s') + ".png" ; } }
//        public ImageSource IconSource { get { return ImageSource.FromFile(Icon); } }
    }

}

