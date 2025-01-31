using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MODELS = Portfi.Data.Models;
using ENUMS = Portfi.Common.Enums;
using RESPONSES = Portfi.Infrastructure.Models.Responses;
using SERVICES = Portfi.Infrastructure.Services.Interfaces;
using System.Net.Http.Headers;

namespace Portfi.Core.Controllers;

/// <summary>
/// Contains API operations for working with projects.
/// </summary>
/// <response code="400">When request malforms.</response>
/// <response code="401">When authentication fails.</response>
/// <response code="500">When an internal server error occurres.</response>
/// <param name="projectService">the project service</param>
/// <param name="logger">the logger</param>
[ApiController]
[Route("api/project")]
[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RESPONSES.ErrorResponse))]
public class ProjectController(
    SERVICES.IProjectService projectService,
    ILogger<ProjectController> logger)
    : ControllerBase
{
    #region GET Requests:

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
    async public Task<IActionResult> GetGitHubProjectsByUsername(
        [Required]
        [FromQuery(Name = "username")]
        string username)
    {
        return Ok();
    }

    #endregion

    #region POST Requests:

    /// <summary>
    /// Adds a descriptipn to a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="description">the description</param>
    /// <returns>The project that had description added.</returns>
    /// <response code="200">Returns the project that had description added to it.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("addProjectDescription")]
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
    /// Adds an active link to a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="activeLink">the active link</param>
    /// <returns>The project that had active link added.</returns>
    /// <response code="200">Returns the project that had active link added.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("addActiveLinkToProject")]
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
    /// Adds categories to a project by specified project ID.
    /// </summary>
    /// <param name="projectId">the project ID</param>
    /// <param name="categories">the categories</param>
    /// <returns>The project that had categories added.</returns>
    /// <response code="200">Returns the project that had categories added.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("addCategoriesToProject")]
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

        _ = Enum.TryParse(categories[0], out ENUMS.ProjectCategory myEnum);

        foundProject.Categories = new HashSet<ENUMS.ProjectCategory>([myEnum]);

        return Ok(foundProject);
    }

    #endregion

    #region PATCH Requests:

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

        _ = Enum.TryParse("Active", out ENUMS.ProjectCategory myEnum);

        foundProject.Categories = new HashSet<ENUMS.ProjectCategory>([myEnum]);

        return Ok(foundProject);
    }

    #endregion

    #region DELETE Requests:

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

    #endregion
}
