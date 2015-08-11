<?php

/*

  Plugin Name: JSON API mNews
  Plugin URI:
  Description: Extends the JSON API Plugin for RESTful FSoft mobile news
  Version: 1.0
  Author: FSB
  Author URI: http://www.fsoft.com.vn
  License:

 */

include_once( ABSPATH . 'wp-admin/includes/plugin.php' );


define("OPTION_UPDATE", 1);//dont need to update
define("FORCE_UPDATE", 2);//need to update

define("ANDROID_CURRENT_VERSION_CODE", 2);
define("ANDROID_CURRENT_VERSION_LINK","https://gateway.fsoft.com.vn");
define("ANDROID_UPDATE_TYPE",0);
define("IOS_CURRENT_VERSION_CODE", 2);
define("IOS_CURRENT_VERSION_LINK","https://gateway.fsoft.com.vn");

define("BREAKING_NEWS_CATEGORY_ID", 86);
define("HOME_GROUP_POST_PER_GROUP", 3);

define("UPDATE_CLIENT_MESSAGE", "Please update your app");
define('JSON_API_MNEWS_HOME', dirname(__FILE__));

$excluded_categories = array(BREAKING_NEWS_CATEGORY_ID); /* Slide Main */
$categories_ordered = array(
		1=>0, //Announcement
		2=>1, //Tech in Use
		167=>2, //Tech in Use
		3 => 3, //News
		168 => 4, //Media Room
		212 => 5, //Fun and Games

		//local test
// 		14=>9,
// 		11=>7,
// 		2=>8
);

if (!is_plugin_active('json-api/json-api.php')) {

    add_action('admin_notices', 'pim_mnews_draw_notice_json_api');

    return;
}

add_filter('json_api_controllers', 'pimFNewsJsonApiController');
add_filter('json_api_fnews_controller_path', 'setFNewsControllerPath');
add_action('init', 'json_api_auth_checkAuthCookie', 100);
load_plugin_textdomain('json-api-mnews', false, basename(dirname(__FILE__)) . '/languages');

function pim_mnews_draw_notice_json_api() {

    echo '<div id="message" class="error fade"><p style="line-height: 150%">';

    _e('<strong>JSON API mNews</strong></a> requires the JSON API plugin to be activated. Please <a href="https://wordpress.org/plugins/json-api/">install / activate JSON API</a> first.', 'json-api-user');

    echo '</p></div>';
}

function pimFNewsJsonApiController($aControllers) {
    $aControllers[] = 'FNews';
    return $aControllers;
}


function setFNewsControllerPath($sDefaultPath) {
    return dirname(__FILE__) . '/controllers/FNews.php';
}

function mnews_get_category_index($response) {
	global $excluded_categories; //mNews options
	//var_dump($response);
	if (isset($response['categories'])) {
    	foreach ($response['categories'] as $wp_category)
    		if (in_array($wp_category->id, $excluded_categories)) {
      			$wp_category->visible = 0;
    		} else  {
    			$wp_category->visible = 1;
    		}

    	//order categories
    	usort($response['categories'], "category_sort");
  	}

  return $response;
}

function category_sort($a,$b) {
	global $categories_ordered;
	//var_dump($categories_ordered);
	if (isset($categories_ordered[$a->id])){
		if (isset($categories_ordered[$b->id])){
			return $categories_ordered[$a->id] > $categories_ordered[$b->id];
		} else {
			return -1;
		}
	} else {
		if (isset($categories_ordered[$b->id])){
			return 1;
		} else {
			return 0;
		}
	}
	return 0;
}

add_filter('json_api_encode', 'mnews_get_category_index');

add_filter('json_api_encode', 'fnews_encode_kento_vote');

function fnews_encode_kento_vote($response) {
  if (isset($response['posts'])) {
    foreach ($response['posts'] as $post) {
      //fnews_add_kentovote_status($post); // dont need to add - get status when show detail
    }
  } else if (isset($response['post'])) {
    fnews_add_kentovote_status($response['post']); // Add a kittens property
  }
  return $response;
}

function fnews_add_kentovote_status($post) {
  //var_dump($post);
  $post->kento_vote = array(
  	"vote_status" => (int)kentovote_voted_status($post->id),
  	"vote_up_total"	=> (int)kento_vote_count($post->id, 1),
  	"vote_down_total"	=> (int)kento_vote_count($post->id, 2)
  );
}

function kentovote_voted_status($postid)
{
	//return $postid;
	if ( is_user_logged_in() ) {
		//$postid = get_the_ID();
		global $wpdb;
		$userid = get_current_user_id();
    	$table = $wpdb->prefix . "kento_vote_info";

		$result = $wpdb->get_results("SELECT votetype FROM $table WHERE userid = '$userid' AND postid = '$postid' ", ARRAY_A);//
		$voted_status = $result[0]['votetype'];
		/*
		if($voted_status==1){
			return "upvoted";
			}
		elseif($voted_status==2){
			return "downvoted";
			}
		}
		*/
		return $voted_status;
	}

	return -1;
}

function kento_vote_count($postid, $votetype)
{
		global $wpdb;
    	$table = $wpdb->prefix . "kento_vote_info";
		$result = $wpdb->get_results("SELECT count(userid) as count FROM $table WHERE postid = '$postid' AND votetype='$votetype'", ARRAY_A);

		//echo "SELECT count(userid) as count FROM $table WHERE postid = '$postid' AND votetype='$votetype'" . "\n";
		//var_dump($result);

		$total_vote =$result[0]['count'];// $wpdb->num_rows;//

		return $total_vote;
}
