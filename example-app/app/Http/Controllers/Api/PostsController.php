<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Post;
use Illuminate\Http\Request;

class PostsController extends Controller
{
    // return posts from https://jsonplaceholder.typicode.com/posts and store them in the Post model

    public function index(Request $request)
    {
        $posts = json_decode(file_get_contents('https://jsonplaceholder.typicode.com/posts'));

        foreach ($posts as $post) {
            $post = Post::create([
                'title' => $post->title,
                'userId' => $post->userId,
                'completed' => $post->completed,
            ]);
        }

        return response()->json([
            'posts' => $posts,
        ]);
    }
}
