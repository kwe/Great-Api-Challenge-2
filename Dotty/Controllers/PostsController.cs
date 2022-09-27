using Microsoft.AspNetCore.Mvc;
using Dotty.Clients;

namespace Dotty.Api.Controllers;

[ApiController]
public class DottyApiController : ControllerBase
{
    private readonly IPostsClient _postClient;

    public DottyApiController(IPostsClient postClient)
    {
        _postClient = postClient;
    }

    [HttpGet("posts")]
    public async Task<IActionResult> Posts()
    {
        try
        {
            var weather = await _postClient.GetPostsAsync();
            return weather is not null ? Ok(weather) : NotFound();
        }
        catch
        {
            return NotFound();
        }
    }
}
