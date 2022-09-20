<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class HelloController extends Controller
{
    public function index(Request $request)
    {
        return response()->json([
            'message' => 'Hello, ' . $request->name,
        ]);
    }
}
