<?php

/*
Controller Name: FNews
Controller Description: FSoft News add-on controller for the Wordpress JSON API plugin
Controller Author: FSB
*/
class JSON_API_FNews_Controller {
	public function check_update() {
		global $json_api;
		$os = $json_api->query->os;
		$version_code = $json_api->query->version_code;

		$ret = array ();
		if (empty ( $os ) || empty ( $version_code )) {
			$ret ["error"] = 1;
			$ret ["msg"] = "Invalid Params";
		} else {
			// var_dump($json_api->query->count);
			$ret ["error"] = 0;

			switch ($os) {
				case "android" :
					if ($version_code < ANDROID_CURRENT_VERSION_CODE) {
						$ret ["update_info"] = array (
								"link" => ANDROID_CURRENT_VERSION_LINK,
								"message" => UPDATE_CLIENT_MESSAGE ,
								"type" => FORCE_UPDATE
						);
					}
					break;
				case "ios" :
					if ($version_code < IOS_CURRENT_VERSION_CODE) {
						$ret ["update_info"] = array (
								"link" => IOS_CURRENT_VERSION_LINK,
								"message" => UPDATE_CLIENT_MESSAGE,
								"type" => FORCE_UPDATE
						);
					}
					break;
				default :
					$ret ["error"] = 1;
					$ret ["msg"] = "Invalid Params";
					break;
			}
		}
		return $ret;
	}

	public function home_posts() {
		global $json_api;
		$cats = array ();
		// $count = $json_api->query->count or 3;
		// if ($json_api->query->cat_ids) {
		// $cats = explode(",",$json_api->query->cat_ids);
		// } else {

		// }

		global $excluded_categories;
		$categories = $json_api->introspector->get_categories ();
		usort($categories, "category_sort");

		foreach ( $categories as $cat )
			if ($cat->parent == 0 && $cat->post_count > 0) {
// 				if (in_array ( $wp_category->id, $excluded_categories ) && $cat->id != BREAKING_NEWS_CATEGORY_ID)
// 					continue;
				if ($cat->id == BREAKING_NEWS_CATEGORY_ID) {
					$cat->breaking_news = 1;
				} else {
					$cat->breaking_news = 0;
				}
				$cats [] = $cat;
			}

			// var_dump($cats);
		$home_posts = array ();

		foreach ( $cats as $cat ) {
// 			var_dump($cat);
			$cat_id = $cat->id;
// 			if ($cat_id == 86) continue;
			$post_query = array (
					'cat' => $cat_id,
					'paged' => 1,
					'posts_per_page' =>  HOME_GROUP_POST_PER_GROUP
			);
			//khong hieu sao bi cache khi su dung API cua WP
			$posts = query_posts($post_query);//$json_api->introspector->get_posts ( $post_query );

			//var_dump($posts);
			if ($cat->post_count > 0){
				//echo $cat->title ."   ".count($posts);
				// 				var_dump($posts);
			}

			$ret_posts = $this->posts_result ( $posts );



			$home_posts [] = array (
					"category" => $cat,
					"posts" => $ret_posts
			);
			$posts = null;
		}

		return array (
				"home_posts" => $home_posts
		);
	}
	protected function posts_result($posts) {
		global $wp_query;
		$ret_posts = array ();
		foreach ( $posts as $post ) {
			$post = new JSON_API_Post($post);
			$p = array (
					"id" => $post->id,
					"title" => $post->title,
					"title_plain" => $post->title_plain,
					"date" => $post->date,
					"modified" => $post->modified
			);

			if (isset ( $post->thumbnail_images ))
				$p ["thumbnail_images"] = $post->thumbnail_images;
			if (isset ( $post->attachments ))
				$p ["attachments"] = $post->attachments;
			$ret_posts [] = $p;
		}
		return $ret_posts;
	}

	function get_comments(){
		global $json_api;
		if (! $json_api->query->post_id) {
			$json_api->error ( "No post specified. Include 'post_id' var in your request." );
		}

		$comments = $json_api->introspector->get_comments($json_api->query->post_id);
		//var_dump($comments);
		return array("comments"=>$comments);
	}

	// respond submit_comment method work incorect - user_id alway is zero :(
	function post_comment() {
		global $json_api;

		if (! $json_api->query->post_id) {
			$json_api->error ( "No post specified. Include 'post_id' var in your request." );
		}

		if (! $json_api->query->user_id) {
			$json_api->error ( "Please include 'user_id' var in your request." );
			if ($json_api->query->user_id == -1) {//without login
				$comment_author = $json_api->query->comment_author;
				$comment_author_email = $json_api->query->comment_author_email;
			} else {
				$comment_author = $user_info->display_name?$user_info->display_name:$user_info->user_login;
				$comment_author_email = $user_info->user_email;
			}
		}

		if (! $json_api->query->content) {
			$json_api->error ( "Please include 'content' var in your request." );
		}

		$user_info = get_userdata ( $json_api->query->user_id );

		$time = current_time ( 'mysql' );
		$agent = $_SERVER ['HTTP_USER_AGENT'];
		$ip = $_SERVER ['REMOTE_ADDR'];
		//var_dump($user_info);
		$data = array (
				'comment_post_ID' => $json_api->query->post_id,
				'comment_author' => $comment_author,
				'comment_author_email' => $comment_author_email,
				'comment_author_url' => $user_info->user_url,
				'comment_content' => $json_api->query->content,
				'comment_type' => '',
				'comment_parent' => 0,
				'user_id' => $json_api->query->user_id,
				'comment_author_IP' => $ip,
				'comment_agent' => $agent,
				'comment_date' => $time,
				'comment_approved' => 1
		);

		// print_r($data);

		$comment_id = wp_insert_comment ( $data );
		$comment = get_comment ( $comment_id );
		$status = ($comment->comment_approved) ? 'ok' : 'pending';
		$new_comment = new JSON_API_Comment ( $comment );
		$json_api->response->respond ( $new_comment, $status );
	}

	// FTV - JW Playlist
	function get_jw_playlist() {
		global $json_api;
		// jw_playlist
		$query = array (
				'post_type' => 'jw_playlist',
				'post_status' => array (
						'publish',
						'pending',
						'draft',
						'auto-draft',
						'future',
						'private',
						'inherit',
						'trash'
				)
		);
		$posts = query_posts ( $query );
		$output = array();
		foreach ( $posts as $post ) {
			$new_post = new JSON_API_Post ( $post );
			$output [] = $new_post;
		}
		return array(
				"count"=>sizeof($output),
				"pages"=>1,
				"posts"=>$output);
		// var_dump($posts);
		// return $posts;
	}

	function get_jw_contents() {
		global $json_api;

		$ids = $json_api->query->ids;
		$query = array (
				'post__in'=>explode(",", $ids),
				'post_type' => 'attachment',
				'post_status' => array (
						'attachment'
				)
		);
		$posts = query_posts ( $query );
		$output = array();
		foreach ( $posts as $post ) {
			//var_dump($post);
			$p = new JSON_API_Post($post);
			$p->guid = $post->guid;
			$output[] = $p;
		}
		return array(
				"posts"=>$output);

		//return $posts;
	}
}
