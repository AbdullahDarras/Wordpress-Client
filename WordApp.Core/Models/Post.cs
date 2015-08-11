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
using System.Text.RegularExpressions;
using ModernHttpClient;
using System.Net.Http;


namespace FSoft.WordApp.Core.Models
{
	public class CustomFields
	{
		public List<string> allegro_page_print { get; set; }
		public List<string> allegro_page_email { get; set; }
		public List<string> allegro_breaking_post { get; set; }
		public List<string> allegro_breaking_slider { get; set; }
		public List<string> allegro_updated_tag { get; set; }
		public List<string> allegro_sidebar_select_small { get; set; }
		public List<string> allegro_sidebar_position_small { get; set; }
		public List<string> allegro_similar_posts { get; set; }
		public List<string> allegro_single_image { get; set; }
		public List<string> allegro_share_single { get; set; }
		public List<string> allegro_about_author { get; set; }
		public List<string> allegro_highlights { get; set; }
		public List<string> allegro_post_views_count { get; set; }
	}

	public class ThumbnailImages
	{
		public ImageWP Full { get; set; }
		public ImageWP Thumbnail { get; set; }
		public ImageWP Medium { get; set; }
		public ImageWP Large { get; set; }
	}

	public class Tag
	{
		public int Id { get; set; }
		public string Slug { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int Post_count { get; set; }
	}


	public class Post
	{
		public int Id { get; set; }
		public string Type { get; set; }
		public string Slug { get; set; }
		public string Url { get; set; }
		public string Status { get; set; }
		public string Title { get; set; }
		private string _Title_plain;
		public string Title_plain { 
			get { return _Title_plain;} 
			set { 
				_Title_plain = System.Net.WebUtility.HtmlDecode (value);
			}
		}
		public string Content { get; set; }

		private string excerpt;
		public string Excerpt { get { return excerpt;} 
			set { 
				excerpt  = value==null?"":Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
				if (excerpt.StartsWith ("Follow Unfollow")) {
					excerpt = excerpt.Remove (0, excerpt.IndexOf ("Followers") + 12);
				}
				excerpt = System.Net.WebUtility.HtmlDecode (excerpt);
			} 
		}
		public string Date { get; set; }
		public string Modified { get; set; }
		private List<Category> _Categories;
		public List<Category> Categories {
			get {
				return _Categories;
			} 
			set	{
				_Categories = value;
				if (_Categories != null && _Categories.Count > 0) {
					FirstCategoryName = _Categories.ToArray () [_Categories.Count - 1].Title;//get last category - refer to sub-cat :))
				}
			}
		}
		public List<Tag> Tags { get; set; }
		public Author Author { get; set; }
		public List<Comment> Comments { get; set; }

		private List<Attachment> _Attachments;
		public List<Attachment> Attachments { 
			get { 
				return _Attachments;
			} 
			set { 
				_Attachments = value;
				if (_Attachments != null && _Attachments.Capacity > 0) {
					//_Attachments [0].Url =	_Attachments [0].Url.Replace ("techinsight.web.fsoft.com.vn", "techinsight.fsoft.com.vn");
					for (int i = 0; i < _Attachments.Capacity; i++) 
						if (_Attachments[i].Mime_type.Contains("image")) {
							IconSource = _Attachments [i].Url;
							FullImage = _Attachments [i].Url;
							break;
						}
				}
			} 
		}
		public int Comment_count { get; set; }
		public string comment_status { get; set; }
		public string Thumbnail { get; set; }
		public CustomFields custom_fields { get; set; }
		public string thumbnail_size { get; set; }
		private ThumbnailImages _Thumbnail_images;
		public ThumbnailImages __Thumbnail_images {
			get { 
				return _Thumbnail_images;
			} 
			set {
				_Thumbnail_images = value; 
				if (_Thumbnail_images != null) {
					if (_Thumbnail_images.Large != null) {
						IconSource = _Thumbnail_images.Large.Url;
					} else if (_Thumbnail_images.Medium != null) {
						IconSource = _Thumbnail_images.Medium.Url;
					} else if (_Thumbnail_images.Full != null) {
						IconSource = _Thumbnail_images.Full.Url;
					} else {
						IconSource = string.Empty;
					}

					if (_Thumbnail_images.Full != null) {
						FullImage = _Thumbnail_images.Full.Url;
					}
				} else {
					IconSource = string.Empty;
				}

				if (string.IsNullOrEmpty (FullImage)) {
					FullImage = IconSource;
				}
			} 
		}

		private string _IconSource;
		public string IconSource {
			get {
				return _IconSource;
			}
			set { 
				_IconSource = value;
				if (!Settings.WP_USED_FEATURED_IMAGE) {
					if (!string.IsNullOrEmpty (_IconSource))
						_IconSource = _IconSource.Insert (_IconSource.LastIndexOf ('.'), "-150x150");
				}
			}
		}

		private string _FullImage;
		public string FullImage {
			get {
				return _FullImage;
			}
			set { 
				_FullImage = value;
			}
		}
		public string GUID { get; set; }


		public bool IsUpVoted;
		public bool IsDownVoted;
		public bool IsNotVoted;
		private KentoVote _Kento_vote;
		public KentoVote Kento_vote { 
			get{ return _Kento_vote;}
			set { 
				_Kento_vote = value;
				if (_Kento_vote != null) {
					IsUpVoted = (_Kento_vote.Vote_status == 1);
					IsDownVoted = !IsUpVoted;
					IsNotVoted = (_Kento_vote.Vote_status == 0);
				}
			}
		}

		public string FirstCategoryName { get; set;}

		public override string ToString ()
		{
			return string.Format ("[Post: id={0}, type={1}, title={2}, excerpt={3} Icon={4}]", Id, Type, Title, Excerpt, IconSource);
		}
	}
}

