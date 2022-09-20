<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class PostsController extends Controller
{
    // return posts from https://jsonplaceholder.typicode.com/posts
    public function index(Request $request)
    {
        $posts = json_decode(file_get_contents('https://jsonplaceholder.typicode.com/posts'));

        return response()->json($posts);
    }
}
