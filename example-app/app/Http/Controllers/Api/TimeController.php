<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class TimeController extends Controller
{
    // return the current time as a JSON object
    public function index(Request $request)
    {
        return response()->json([
            'time' => date('Y-m-d H:i:s'),
        ]);
    }
}
