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
using FSoft.WordApp.Core.Models;
using Cirrious.MvvmCross.ViewModels;
using System.Diagnostics;
using FSoft.WordApp.Core.Services;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Cirrious.Conference.Core.Converters;
using EShyMedia.MvvmCross.Plugins.DeviceInfo;
using Cirrious.CrossCore;
using System.Collections.Generic;

namespace FSoft.WordApp.Core.ViewModels
{
	public class PostViewModel : BaseViewModel
	{
		public Post Post;
		private string _html;
		public string Html { 
			get { return _html;} 
			set { SetProperty (ref _html, value, "Html");}
		}
		private bool isLoading;
		public bool IsLoading {
			get { return isLoading; }
			set { SetProperty (ref isLoading, value, "IsLoading");}
		}

		private string _title;
		public string Title {
			get { return _title;}
			set { SetProperty (ref _title, value, "Title"); }
		}

		private string _newsTitle;
		public string NewsTitle {
			get { return _newsTitle;}
			set { SetProperty (ref _newsTitle, value, "NewsTitle"); }
		}

		//backcommand binding
		private Cirrious.MvvmCross.ViewModels.MvxCommand _BackCommand;
		public System.Windows.Input.ICommand BackCommand
		{
			get
			{
				_BackCommand = _BackCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>Close(this));
				return _BackCommand;
			}
		}
		//like command binding
		public bool SupportLike {get; set;}

		private bool _IsUnLikedThisPost;
		public bool IsUnLikedThisPost {get { return _IsUnLikedThisPost;} set { SetProperty (ref _IsUnLikedThisPost, value, "IsUnLikedThisPost");}}
		private bool _IsLikedThisPost = false;
		public bool IsLikedThisPost {get { return _IsLikedThisPost;} 
			set { 
				SetProperty (ref _IsLikedThisPost, value, "IsLikedThisPost");
				IsUnLikedThisPost = !value;
			}
		}
		private Cirrious.MvvmCross.ViewModels.MvxCommand _LikeCommand;
		public System.Windows.Input.ICommand LikeCommand
		{
			get
			{
				_LikeCommand = _LikeCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>{
					DoLikeCommand();
				});
				return _LikeCommand;
			}
		}
		protected async Task DoLikeCommand() {
			if (IsLikedThisPost) {
				ShowErrorMessage ("You liked this post!");
				return;
			}
			if (Status == NetworkStatus.NotReachable) {//true || 
				ShowErrorMessage (Settings.MSG_NETWORK_NOT_REACHABLE);
				return;
			}
			IsLoading = true;
			try {
				var liked = await Service.LikePost (Post.Id);
				Debug.WriteLine ("Liked: " + liked.LikedCode);

				if (Settings.WP_KENTO_VOTE_RESULT_LIKED.Equals(liked.LikedCode)) {
					//liked
				} else if (Settings.WP_KENTO_VOTE_RESULT_UNLIKED.Equals(liked.LikedCode)){
					//
				}
				Like_count ++;
				IsLikedThisPost = true;
			} catch (Exception e){
				ShowErrorMessage (Settings.MSG_NETWORK_COMMON, e);
			}
			IsLoading = false;
		}

		private Cirrious.MvvmCross.ViewModels.MvxCommand _UnLikeCommand;
		public System.Windows.Input.ICommand UnLikeCommand
		{
			get
			{
				_UnLikeCommand = _UnLikeCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>{
					DoUnLikeCommand();
				});
				return _UnLikeCommand;
			}
		}
		protected async Task DoUnLikeCommand() {
			if (Status == NetworkStatus.NotReachable) {//true || 
				ShowErrorMessage (Settings.MSG_NETWORK_NOT_REACHABLE);
				return;
			}
			IsLoading = true;
			try {
				var liked = await Service.UnLikePost (Post.Id);
				Debug.WriteLine ("UnLiked: " + liked.LikedCode);

				if (Settings.WP_KENTO_VOTE_RESULT_LIKED.Equals(liked.LikedCode)) {
					//liked
				} else if (Settings.WP_KENTO_VOTE_RESULT_UNLIKED.Equals(liked.LikedCode)){
					//
				}
				Like_count --;
				IsLikedThisPost = false;
			} catch (Exception e){
				ShowErrorMessage (Settings.MSG_NETWORK_COMMON, e);
			}
			IsLoading = false;
		}
		//comment command binding
		public event EventHandler CommentPressed;
		private Cirrious.MvvmCross.ViewModels.MvxCommand _ShowCommentCommand;
		public System.Windows.Input.ICommand ShowCommentCommand
		{
			get
			{
				_ShowCommentCommand = _ShowCommentCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>{
					var buildDetail = Mvx.Resolve<IBuildDetails>();
					if (buildDetail != null && buildDetail.OS.Equals(IBuildDetails.OS_IOS)) {
						ShowViewModel<PostCommentsViewModel>(Post);
					} else {
						if (CommentPressed != null) {
							CommentPressed(this, EventArgs.Empty);
						}
					}
				});
				return _ShowCommentCommand;
			}
		}

		//comment post binding
		public string CommentText {get;set;}
		public event EventHandler PostCommentPressed;
		private Cirrious.MvvmCross.ViewModels.MvxCommand _PostCommentCommand;
		public System.Windows.Input.ICommand PostCommentCommand
		{
			get
			{
				_PostCommentCommand = _PostCommentCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>{
					DoPostComment();
				});
				return _PostCommentCommand;
			}
		}

		async public void DoPostComment(){
			if (string.IsNullOrEmpty (CommentText) || string.IsNullOrEmpty(CommentText.Trim())) {
				ShowErrorMessage (Settings.MSG_COMMENT_EMPTY_TEXT_ERROR);
				return;
			}
			if (Status == NetworkStatus.NotReachable) {//true || 
				ShowErrorMessage (Settings.MSG_NETWORK_NOT_REACHABLE);
				return;
			}
			string txtCommentContent = CommentText;
			Debug.WriteLine ("Post comment: " + txtCommentContent);
			if (PostCommentPressed != null) {
				PostCommentPressed(this, EventArgs.Empty);
			}
			IsLoading = true;
			RequestPostComment req = new RequestPostComment (Post.Id, txtCommentContent);
			var res = await Service.PostComment (req);

			try {
				var jo = JsonConvert.DeserializeObject<Comment>(res.Result);
//				var auth = new Author();
//				auth.name = Settings.WP_USERNAME;
//				jo.Author = auth;
//				jo.UserId = auth.id;
//				jo.UserCaption = auth.slug.ToUpper()[0] + "";
				jo.Content = GetCommentText(jo);

				if (Post.Comments == null )
					Post.Comments = new List<Comment>();
				Post.Comments.Add(jo);
				Comments.Add(jo);

				CommentText = string.Empty;
				RaisePropertyChanged(()=>CommentText);
				Comment_count += 1;
			} catch (Exception e) {
				Debug.WriteLine ("[CommentPage] {0}" , e);
				if (PostCommentPressed != null) {
					PostCommentPressed(this, new ErrorEventArgs("Error",e.Message,"close"));
				}
				ShowErrorMessage (Settings.MSG_NETWORK_COMMON, e);
			}
			IsLoading = false;
		}
		//comments binding
		private bool _IsNoComment;
		public bool IsNoComment { get { return _IsNoComment; } set{ SetProperty(ref _IsNoComment, value, "IsNoComment"); }}
		private bool _HasComment;
		public bool HasComment { get{ return _HasComment;} set{SetProperty(ref _HasComment, value, "HasComment");}}
		public ObservableCollection<Comment>  Comments { get; set;}
		private int _Comment_count;
		public int Comment_count { 
			get { 
				return _Comment_count;
			}
			set {
				SetProperty(ref _Comment_count, value, "Comment_count");
				IsNoComment = (value == 0);
				HasComment = !IsNoComment;
			}
		}

		//refresh comments
		private Cirrious.MvvmCross.ViewModels.MvxCommand _RefreshCommentsCommand;
		public System.Windows.Input.ICommand RefreshCommentsCommand
		{
			get
			{
				_RefreshCommentsCommand = _RefreshCommentsCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(()=>{
					DoRefreshCommentsCommand();
				});
				return _RefreshCommentsCommand;
			}
		}
		protected async Task DoRefreshCommentsCommand() {
			if (Status == NetworkStatus.NotReachable) {//true || 
				ShowErrorMessage (Settings.MSG_NETWORK_NOT_REACHABLE);
				return;
			}

			IsLoading = true;

			var req = new RequestComments(Post.Id);
			try{
				var response = await Service.GetComments (req);

				var ref_comments = response.Comments;
				ObservableCollection<Comment> _Comments = new ObservableCollection<Comment>();

				foreach (Comment c in ref_comments) {
					c.Content = GetCommentText(c);
					_Comments.Add(c);
				}

				Comments.Clear();
				Comments = _Comments;
				RaisePropertyChanged(()=>Comments);
			} catch (Exception e){
				Debug.WriteLine (e);
				ShowErrorMessage (Settings.MSG_NETWORK_COMMON, e);
				return;
			}

			IsLoading = false;
		}


		private int _Like_count;
		public int Like_count { 
			get { return _Like_count;}
			set { 
				SetProperty (ref _Like_count, value, "Like_count");
			}
		}

		public PostViewModel(IFNewsService service, IMvxDeviceInfo deviceInfoPlugin) : base(service, deviceInfoPlugin) {
			SupportLike = false;
		}

		public void Init(Post post){
			Post = post;
			Comment_count = Post.Comment_count;
			Title = Post.FirstCategoryName;
			NewsTitle = Post.Title_plain;
			Like_count = 0;
			if (Post.Kento_vote != null)
				Like_count = Post.Kento_vote.Vote_up_total;
			
			Comments = new ObservableCollection<Comment> ();
		}

		public override void Start ()
		{
			base.Start ();
			//LoadPost ();
		}

		public async Task LoadPost() {

			if (Status == NetworkStatus.NotReachable) {//true || 
				ShowErrorMessage (Settings.MSG_NETWORK_NOT_REACHABLE);
				return;
			}

			IsLoading = true;

			RequestPost req = new RequestPost();
			req.Id = Post.Id;
			ResponsePost detailPost;

			try{
				detailPost = await Service.GetPost (req);
				Post = detailPost.Post;
				if (Post.Kento_vote != null) {
					IsLikedThisPost = Post.Kento_vote.Vote_status == 1;
					Like_count = Post.Kento_vote.Vote_up_total;
				}
				Title = Post.FirstCategoryName;//category name - home post
			} catch (Exception e){
				Debug.WriteLine (e);
				ShowErrorMessage (Settings.MSG_NETWORK_COMMON, e);
				return;
			}

			var doc = new HtmlAgilityPack.HtmlDocument();

			//remove ulike info
			try {
				doc.LoadHtml(detailPost.Post.Content);
				foreach(var item in doc.DocumentNode.ChildNodes)
				{
					if (item.Id.StartsWith ("kento-vote")) {
						item.InnerHtml = string.Empty;
						Debug.WriteLine ("Empty ULike Info: " + item.OuterHtml);
					}

					//remove follow & unfollow content
					if (item.Attributes["class"] != null && item.Attributes["class"].Value.StartsWith("wpw-fp-follow-post-wrapper"))
						item.InnerHtml = string.Empty;
				}
				var stringBuilder = new System.Text.StringBuilder();
				doc.Save (new System.IO.StringWriter(stringBuilder));

				Post.Content = stringBuilder.ToString();
			} catch (Exception e) {
				ShowErrorMessage (Settings.MSG_NETWORK_COMMON, e);
			}

			//Repair comment content to plain text
			try {
				Comment_count = Post.Comment_count;
				foreach (Comment comment in Post.Comments) {
					doc.LoadHtml(comment.Content);
					foreach(var item in doc.DocumentNode.ChildNodes)// "//div" is a xpath which means select div nodes that are anywhere in the html

					{
						if (item.Id.StartsWith ("wp-ulike-comment-")) {
							item.InnerHtml = string.Empty;
							Debug.WriteLine (item.OuterHtml);
						}
					}

					comment.Content = System.Net.WebUtility.HtmlDecode (doc.DocumentNode.InnerText);
					Comments.Add (comment);
				}
			} catch (Exception e) {
				Debug.WriteLine ("[CommentPage-LoadComments] {0}", e);
			}
			string featured_img = Post.FullImage; //Post.IconSource==null ? "" : (Post.Thumbnail_images.Full.Url ?? Post.IconSource) ;			
			string timeAgo = new TimeAgoValueConverter ().Convert (Post.Date, null, null, null).ToString();
//			Html = NewsTemplates.DETAIL_TEMPLATE_HEADER + String.Format(NewsTemplates.DETAIL_TEMPLATE_BODY, Post.Title, timeAgo, featured_img ,Post.Content, Post.FirstCategoryName);
			Html = String.Format(NewsTemplates.DETAIL_TEMPLATE, Post.Title, timeAgo, featured_img ,Post.Content, Post.FirstCategoryName);
			//test video player
			//Html = string.Format(NewsTemplates.DETAIL_VIDEO_PLAYER, "http://techslides.com/demos/sample-videos/small.mp4");


			RaisePropertyChanged ("Comment_count");
			RaisePropertyChanged ("Comments");

			IsLoading = false;
		}

		private string GetCommentText(Comment c) {
			var doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(c.Content);
			foreach(var item in doc.DocumentNode.ChildNodes)// "//div" is a xpath which means select div nodes that are anywhere in the html

			{
				if (item.Id.StartsWith ("wp-ulike-comment-")) {
					item.InnerHtml = string.Empty;
					Debug.WriteLine (item.OuterHtml);
				}
			}

			return System.Net.WebUtility.HtmlDecode (doc.DocumentNode.InnerText);
		}
	}
}

