using Microsoft.AspNetCore.Mvc;

namespace Portfi.Core.Controllers;

[ApiController]
[Route("api/portfolio")]
public class PortfolioController(
    ILogger<PortfolioController> logger)
    : ControllerBase
{
    /// <summary>
    /// Makes a cool Get.
    /// </summary>
    /// 
    /// <returns>zero</returns>
    /// <remarks>
    /// Keep in mind:
    ///
    ///     GET /Cool Get
    ///     {
    ///        You only have to be cool
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Its Cool</response>
    /// <response code="404">If not found</response
    [HttpGet(Name = "Cool Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public IActionResult CoolGet()
    {
        return Ok(0);
    }
}
