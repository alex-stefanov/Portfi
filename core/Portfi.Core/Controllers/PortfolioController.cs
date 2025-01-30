using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MODELS = Portfi.Data.Models;
using RESPONSES = Portfi.Infrastructure.Models.Responses;
using SERVICES = Portfi.Infrastructure.Services.Interfaces;
using static Portfi.Infrastructure.Common.Constants.PortfolioConstants;

namespace Portfi.Core.Controllers;

/// <summary>
/// Contains API operations for working with portfolios.
/// </summary>
/// <response code="400">When request malforms.</response>
/// <response code="401">When authentication fails.</response>
/// <response code="500">When an internal server error occurres.</response>
/// <param name="portfolioService">the portfolio service</param>
/// <param name="logger">the logger</param>
[Authorize]
[ApiController]
[Route("api/portfolio")]
[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RESPONSES.ErrorResponse))]
public class PortfolioController(
    SERVICES.IPortfolioService portfolioService,
    ILogger<PortfolioController> logger)
    : ControllerBase
{
    #region GET Requests:

    /// <summary>
    /// Retrieves a list of all portfolio IDs (GUIDs) for a user.
    /// </summary>
    /// <returns>A list of portfolio IDs.</returns>
    /// <response code="200">Returns the list of portfolio IDs.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpGet("getExamplePortfolioIds")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult GetExamplePortfolioIds()
    {
        var examplePortfolioIds = new List<string>
        {
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        };

        return Ok(examplePortfolioIds);
    }

    /// <summary>
    /// Gets a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that was selected.</returns>
    /// <response code="200">Returns the portfolio that was selected.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpGet("getPortfolioById")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult GetPortfolioById(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            PersonId = "ff",
        };

        return Ok(foundPortfolio);
    }

    #endregion

    #region POST Requests:

    /// <summary>
    /// Makes a portfolio for a user.
    /// </summary>
    /// <param name="userId">the user ID</param>
    /// <param name="biograpgy">the biography</param>
    /// <param name="names">the array of names</param>
    /// <returns>The portfolio that was created.</returns>
    /// <response code="200">Returns the portfolio that was created.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("createPortfolio")]
    [ProducesResponseType(typeof(IEnumerable<MODELS.Portfolio>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult CreatePortfolio(
        [Required]
        [FromQuery(Name = "userID")]
        string userId,
        [Required]
        [FromQuery(Name = "biography")]
        string biography,
        [Required]
        [FromQuery(Name = "names")]
        string[] names)
    {
        var newPorfolio = new MODELS.Portfolio()
        {
            PersonId = userId,
            Biography = biography,
            PersonNames = names,
        };

        return Ok(newPorfolio);
    }

    /// <summary>
    /// Adds social links to a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="socialMediaLinks">the serialized social media links</param>
    /// <returns>The portfolio that had social media links added.</returns>
    /// <response code="200">Returns the portfolio that had social media links added.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("addSocialMediaLinks")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult AddSocialMediaLinks(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "socialMediaLinks")]
        string socialMediaLinks)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            PersonId = "ff",
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Uploads a new avatar url to a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="avatarURL">the avatar url to be added</param>
    /// <returns>The portfolio that had an avatar uploaded.</returns>
    /// <response code="200">Returns the portfolio that had an avatar uploaded.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("uploadAvatar")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult UploadAvatar(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "avatarURL")]
        string avatarURL)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            Avatar = avatarURL,
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Uploads a new cv url to a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="cvURL">the cv url to be added</param>
    /// <returns>The portfolio that had an cv uploaded.</returns>
    /// <response code="200">Returns the portfolio that had a cv uploaded.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("uploadCV")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult UploadCV(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "cv")]
        string cvURL)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            CV = cvURL,
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Adds projects to a portfolio by specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="projects">the portfolio ID</param>
    /// <returns>The portfolio that had projects added.</returns>
    /// <response code="200">Returns the portfolio that had projects added to it.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("addProjects")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult AddProjects(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "projects")]
        string[] projects)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {

        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Adds a project to a portfolio by specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="sourceCodeLink">the source code link</param>
    /// <returns>The project that was added.</returns>
    /// <response code="200">Returns the project was added.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("addProjectToPortfolio")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult AddProjectToPortfolio(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "sourceCodeLink")]
        string sourceCodeLink)
    {
        var foundProject = new MODELS.Project()
        {
            SourceCodeLink = sourceCodeLink,
        };

        return Ok(foundProject);
    }

    #endregion

    #region PATCH Requests:

    /// <summary>
    /// Edits a social link from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="socialMediaLinks">the social media link</param>
    /// <returns>The portfolio that had social media link edited.</returns>
    /// <response code="200">Returns the portfolio that had social media link edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("editSocialMediaLink")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult EditSocialMediaLink(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "socialMediaLink")]
        string socialMediaLink)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            PersonId = "ff",
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Edits the biography from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="biography">the biography to be edited</param>
    /// <returns>The portfolio that had the biography edited.</returns>
    /// <response code="200">Returns the portfolio that had the biography edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("editBiography")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult EditBiography(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "biography")]
        string biography)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            Biography = biography,
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Edits the background theme and/or the main color from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="backgroundTheme">the background theme to be edited</param>
    /// <param name="mainColor">the main color to be edited</param>
    /// <returns>The portfolio that had the background theme and/or the main color edited.</returns>
    /// <response code="200">Returns the portfolio that had the background theme and/or the main color edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("editTheme")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult EditTheme(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [FromQuery(Name = "backgroundTheme")]
        string? backgroundTheme,
        [FromQuery(Name = "mainColor")]
        string? mainColor)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            BackgroundTheme = backgroundTheme ?? DefaultBackgroundThemeValue,
            MainColor = mainColor ?? DefaultMainColorValue,
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Sets the background theme and the main color to the default ones from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that had the background theme and the main color set to default.</returns>
    /// <response code="200">Returns the portfolio that had the background theme and the main color set to default.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("setDefaultTheme")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult SetDefaultTheme(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            BackgroundTheme = DefaultBackgroundThemeValue,
            MainColor = DefaultMainColorValue,
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Edits the names from a portfolio by specified project ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that had the names edited.</returns>
    /// <response code="200">Returns the portfolio that had the names edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("editNames")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult EditNames(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "names")]
        string[] names)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            PersonNames = names,
        };

        return Ok(foundPortfolio);
    }

    #endregion

    #region DELETE Requests:

    /// <summary>
    /// Removes a social links from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="socialMediaLink">the social media link to delete</param>
    /// <returns>The portfolio that had social media link removed.</returns>
    /// <response code="200">Returns the portfolio that had social media link removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("removeSocialMediaLink")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult RemoveSocialMediaLink(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId,
        [Required]
        [FromQuery(Name = "socialMediaLink")]
        string socialMediaLink)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            PersonId = "ff",
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Removes the avatar from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that had the avatar removed.</returns>
    /// <response code="200">Returns the portfolio that had the avatar removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("removeAvatar")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult RemoveAvatar(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            Avatar = DefaultAvatarValue,
        };

        return Ok(foundPortfolio);
    }

    /// <summary>
    /// Removes the CV from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that had the CV removed.</returns>
    /// <response code="200">Returns the portfolio that had the CV removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("removeCV")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult RemoveCV(
        [Required]
        [FromQuery(Name = "portfolioID")]
        string portfolioId)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {
            CV = null,
        };

        return Ok(foundPortfolio);
    }

    #endregion
}