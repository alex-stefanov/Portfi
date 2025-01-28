using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MODELS = Portfi.Data.Models;
using RESPONSES = Portfi.Core.Models.Responses;
using static Portfi.Data.Common.Constants.PortfolioConstants;
using Portfi.Data.Common.Enums;
namespace Portfi.Core.Controllers;

[ApiController]
[Route("api/portfolio")]
public class PortfolioController(
    ILogger<PortfolioController> logger)
    : ControllerBase
{
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
    /// Uploads a new avatar url to a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="avatarURL">the avatar url to be added</param>
    /// <returns>The portfolio that had an avatar uploaded.</returns>
    /// <response code="200">Returns the portfolio that had an avatar uploaded.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("uploadAvatar")]
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
    /// Uploads a new cv url to a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="cvURL">the cv url to be added</param>
    /// <returns>The portfolio that had an cv uploaded.</returns>
    /// <response code="200">Returns the portfolio that had a cv uploaded.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("uploadCV")]
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
    /// Gets public GitHub projects by username.
    /// </summary>
    /// <param name="username">the username</param>
    /// <returns>The gotten GitHub projects.</returns>
    /// <response code="200">Returns the GitHub projects gotten by the username.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpGet("getGitHubProjectsByUsername")]
    [ProducesResponseType(typeof(IEnumerable<RESPONSES.GitHubRepository>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult GetGitHubProjectsByUsername(
        [Required]
        [FromQuery(Name = "username")]
        string username)
    {
        //GET /users/{username}/repos

        var gitHubRepositories = new HashSet<RESPONSES.GitHubRepository>()
        {
            new()
            {
                Id = 12345,
                Name = "Test",
                FullName = "TestovTest",
                Description = "Description",
                HtmlUrl = "https://ff",
                Language = "C#",
            },
            new()
            {
                Id = 123456,
                Name = "Test1",
                FullName = "TestovTest1",
                Description = "Description1",
                HtmlUrl = "https://ff1",
                Language = "F#",
            },
            new()
            {
                Id = 1234567,
                Name = "Test11",
                FullName = "TestovTest11",
                Description = "Description11",
                HtmlUrl = "https://ff11",
                Language = "Q#",
            }
        };

        return Ok(gitHubRepositories);
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
    [HttpPut("addProjects")]
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
    /// Adds a descriptipn to a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="description">the description</param>
    /// <returns>The project that had description added.</returns>
    /// <response code="200">Returns the project that had description added to it.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPut("addProjectDescription")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult AddProjectDescription(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "description")]
        string description)
    {
        var foundProject = new MODELS.Project()
        {
            Description = description,
        };

        return Ok(foundProject);
    }

    /// <summary>
    /// Edits a descriptipn of a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="description">the new description</param>
    /// <returns>The project that had description edited.</returns>
    /// <response code="200">Returns the project that had description edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("editProjectDescription")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult EditProjectDescription(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "description")]
        string description)
    {
        var foundProject = new MODELS.Project()
        {
            Description = description,
        };

        return Ok(foundProject);
    }

    /// <summary>
    /// Removes a descriptipn of a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <returns>The project that had description removed.</returns>
    /// <response code="200">Returns the project that had description removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("removeProjectDescription")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult RemoveProjectDescription(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId)
    {
        var foundProject = new MODELS.Project()
        {
            Description = null,
        };

        return Ok(foundProject);
    }

    /// <summary>
    /// Adds an active link to a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="activeLink">the active link</param>
    /// <returns>The project that had active link added.</returns>
    /// <response code="200">Returns the project that had active link added.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPut("addActiveLinkToProject")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult AddActiveLinkToProject(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "activeLink")]
        string activeLink)
    {
        var foundProject = new MODELS.Project()
        {
            HostedLink = activeLink,
        };

        return Ok(foundProject);
    }

    /// <summary>
    /// Edits an active link to a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="activeLink">the new active link</param>
    /// <returns>The project that had active link edited.</returns>
    /// <response code="200">Returns the project that had active link edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("editActiveLinkToProject")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult EditActiveLinkToProject(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "activeLink")]
        string activeLink)
    {
        var foundProject = new MODELS.Project()
        {
            HostedLink = activeLink,
        };

        return Ok(foundProject);
    }

    /// <summary>
    /// Removes the active link from a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <returns>The project that had active link removed.</returns>
    /// <response code="200">Returns the project that had active link removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("removeActiveLinkFromProject")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult RemoveActiveLinkFromProject(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId)
    {
        var foundProject = new MODELS.Project()
        {
            HostedLink = null,
        };

        return Ok(foundProject);
    }

    /// <summary>
    /// Removes the specified project from a portfolio by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <returns>The portfolio that had the project which was removed.</returns>
    /// <response code="200">Returns the portfolio that had the project which was removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("removeProjectByProjectId")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult RemoveProjectByProjectId(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId)
    {
        var foundPortfolio = new MODELS.Portfolio()
        {

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

    /// <summary>
    /// Adds a project to a portfolio by specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="sourceCodeLink">the source code link</param>
    /// <returns>The project that was added.</returns>
    /// <response code="200">Returns the project was added.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPut("addProjectToPortfolio")]
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

    /// <summary>
    /// Adds categories to a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="categories">the categories</param>
    /// <returns>The project that had categories added.</returns>
    /// <response code="200">Returns the project that had categories added.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPut("addCategoriesToProject")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult AddCategoriesToProject(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "categories")]
        string[] categories)
    {
        var foundProject = new MODELS.Project();

        _ = Enum.TryParse(categories[0], out ProjectCategory myEnum);

        foundProject.Categories = new HashSet<ProjectCategory>([myEnum]);

        return Ok(foundProject);
    }

    /// <summary>
    /// Edits categories from a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="categories">the categories</param>
    /// <returns>The project that had categories edited.</returns>
    /// <response code="200">Returns the project that had categories edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("editProjectCategories")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult EditProjectCategories(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "categories")]
        string[] categories)
    {
        var foundProject = new MODELS.Project();

        _ = Enum.TryParse("Active", out ProjectCategory myEnum);

        foundProject.Categories = new HashSet<ProjectCategory>([myEnum]);

        return Ok(foundProject);
    }

    /// <summary>
    /// Removes all categories from a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <returns>The project that had all categories removed.</returns>
    /// <response code="200">Returns the project that had all categories removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("clearProjectCategories")]
    [ProducesResponseType(typeof(MODELS.Project), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public IActionResult ClearProjectCategories(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId)
    {
        var foundProject = new MODELS.Project
        {
            Categories = []
        };

        return Ok(foundProject);
    }
}
