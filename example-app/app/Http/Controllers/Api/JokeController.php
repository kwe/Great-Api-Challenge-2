<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class JokeController extends Controller
{
    public function index(Request $request)
    {
        return response()->json([
            'Joke' => "What's the fastest cake? Scone!" . $request->name,
        ]);
    }
}
