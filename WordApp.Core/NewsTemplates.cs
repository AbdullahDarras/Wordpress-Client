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

namespace FSoft.WordApp.Core
{
	public class NewsTemplates
	{
		public NewsTemplates ()
		{
		}

		/* Edit and paste detail_template.htmp here */
		public const string DETAIL_TEMPLATE_HEADER = "<html>\n<head>\n\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\n\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no\" />\n\t<style type=\"text/css\">\n\t\timg {\n\t\t\tdisplay: inline; \n\t\t\theight: auto;\n\t\t\tmax-width: 100%;\n\t\t\talign: center;\n\t\t}\n\t\t#image_header {\n\t\t\twidth:100%;\n\t\t\t#border-style: solid;\n    \t\tborder-bottom: 3px solid #4EAE46;\n\t\t}\n\t\t#detail {\n\t\t\tfont-size: 20;\n\t\t\tline-height: 1.4em;\n\t\t\tcolor: #333333;\n\t\t\tbackground-color: transparent;\n\t\t\tword-wrap: break-word;\n\t\t\tmargin:15px;\n\t\t}\n\t\t#title {\n\t\t\tfont-size: 32;\n\t\t\tline-height: 1.4em;\n\t\t\tfont-weight: bold;\n\t\t\tcolor: #222222;\n\t\t\tbackground-color: transparent;\n\t\t\tword-wrap: break-word;\n\t\t\tmargin:15px;\n\t\t}\n\t\t#category {\n\t\t\tfont-size: 18;\n\t\t\tline-height: 1.4em;\n\t\t\tfont-weight: bold;\n\t\t\tcolor: #4EAE46;\n\t\t\tbackground-color: transparent;\n\t\t\tword-wrap: break-word;\n\t\t\tmargin:15px;\n\t\t\ttext-transform: uppercase;\n\t\t}\n\t\t#time {\n\t\t\tfont-size: 15; \n\t\t\tcolor: #7e7e7e; \n\t\t\tmargin:15px;\n\t\t\talign: right;\n\t\t}\n\t\tbody {background-color: #F1F5F7;\n\t\t\tmargin:0;\n\t\t\tpadding:0;\n\t\t\tfont-family: Roboto;\n\t\t}\n\t</style>\n</head>";
		public const string DETAIL_TEMPLATE_BODY = "<body>\n\t<img id=\"image_header\" src=\"{2}\"/>\n\n\t\n\t<div id=\"title\">{0}</div>\n\n\t<table width=\"100%\" border=\"0\">\n\t\t<tr>\n\t\t\t<td><div id=\"category\">{4}</div></td>\n\t\t\t<td align=\"right\"><div id=\"time\">{1}</div></td>\n\t\t</tr>\n\t</table>\n\n\t<div id=\"detail\">{3}</div>\n\t<br>\n\t<br>\n\t<br>\n\t<br>\n</body>\n</html>";	
	



		public const string DETAIL_TEMPLATE = "<html>\n<html>\n<head>\n\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\n\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no\" />\n\t<style type=\"text/css\">\n\t\timg {{\n\t\t\tdisplay: inline; \n\t\t\theight: auto;\n\t\t\tmax-width: 100%;\n\t\t\talign: center;\n\t\t}}\n\t\t#image_header {{\n\t\t\twidth:100%;\n\t\t\t#border-style: solid;\n    \t\tborder-bottom: 3px solid #0A1D8A;\n\t\t}}\n\t\t#detail {{\n\t\t\tfont-size: 20;\n\t\t\tline-height: 1.4em;\n\t\t\tcolor: #333333;\n\t\t\tbackground-color: transparent;\n\t\t\tword-wrap: break-word;\n\t\t\tmargin:20px;\n\t\t}}\n\t\t#title {{\n\t\t\tfont-size: 32;\n\t\t\tline-height: 1.3em;\n\t\t\tfont-weight: bold;\n\t\t\tcolor: #222222;\n\t\t\tbackground-color: transparent;\n\t\t\tword-wrap: break-word;\n\t\t\tmargin:20px;\n\t\t}}\n\t\t#category {{\n\t\t\tfont-size: 18;\n\t\t\tline-height: 1.4em;\n\t\t\tfont-weight: normal;\n\t\t\tcolor: #000098;\n\t\t\tbackground-color: transparent;\n\t\t\tword-wrap: break-word;\n\t\t\tmargin-top:0px;\n\t\t\tmargin-bottom:0px;\n\t\t\tmargin-left:20px;\n\t\t\ttext-transform: uppercase;\n\t\t}}\n\t\t#time {{\n\t\t\tfont-size: 15; \n\t\t\tcolor: #7e7e7e; \n\t\t\tmargin-top:0px;\n\t\t\tmargin-bottom:0px;\n\t\t\tmargin-right:20px;\n\t\t\talign: right;\n\t\t}}\n\t\tbody {{\n\t\t\tbackground-color: #F1F5F7;\n\t\t\tmargin:0;\n\t\t\tpadding:0;\n\t\t\tfont-family: Roboto;\n\t\t}}\n\t</style>\n</head>\n<body>\n\t<img id=\"image_header\" src=\"{2}\"/>\n\n\t\n\t<div id=\"title\">{0}</div>\n\n\t<table width=\"100%\" border=\"0\">\n\t\t<tr>\n\t\t\t<td><div id=\"category\">{4}</div></td>\n\t\t\t<td align=\"right\"><div id=\"time\">{1}</div></td>\n\t\t</tr>\n\t</table>\n\n\t<div id=\"detail\">{3}</div>\n\t<br>\n\t<br>\n\t<br>\n\t<br>\n</body>\n</html>";



		public const string DETAIL_VIDEO_PLAYER = "<html>\n<head>\n\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\n\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no\" />\n</head>\n<body>\n\n\t<video controls>\n\t\t<source src=\"{0}\">\n\t</video>\n</body>\n</html>";

		public const string COMMENT_TEMPLATE_HEADER = "<html>\n<head>\n\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\n\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no\" />\n\t<style type=\"text/css\">\n\timg {\n\t\tmax-width: 90%;\n\t\tpadding: 10px;\n\t\talign: center;\n\t}\n\t#detail {\n\t\tfont-family: Georgia;\n\t\tfont-size: 20;\n\t\tline-height: 1.4em;\n\t\tcolor: #555555;\n\t\tbackground-color: transparent;\n\t\tword-wrap: break-word;\n\t}\n\t</style>\n</head>\n<body>";
		public const string COMMENT_TEMPLATE_TITLE = "<span style=\"font-weight: bold; font-size: 24; color: #222222\">{0}</span> <!-- post title -->";
		public const string COMMENT_TEMPLATE_FOOTER = "</body>\n</html>";
		public const string COMMENT_TEMPLATE_ITEM = "<table border=\"0\">\n\t<tr>\n\t\t<td><b>{0}</b></td> <!-- Author name -->\n\t</tr>\n\t<tr>\n\t\t<td>{1}</td> <!-- date -->\n\t</tr>\n\t<tr>\n\t\t<td><br/></td>\n\t</tr>\n\t<tr>\n\t\t<td>{2}</td>\n\t</tr>\n\t</table>\n\n\t<hr/>";
	}
}

