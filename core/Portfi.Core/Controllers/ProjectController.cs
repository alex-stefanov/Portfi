using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using Supabase;
using Microsoft.AspNetCore.Mvc;
using Portfi.Common.Helpers;
using MODELS = Portfi.Data.Models;
using ENUMS = Portfi.Common.Enums;
using DTO = Portfi.Common.Dto;
using EXCEPTIONS = Portfi.Common.Exceptions;
using RESPONSES = Portfi.Infrastructure.Models.Responses;
using SERVICES = Portfi.Infrastructure.Services.Interfaces;

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
    Client supabase,
    ILogger<ProjectController> logger)
    : ControllerBase
{
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://api.github.com/"),
    };

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
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            httpClient.DefaultRequestHeaders.UserAgent
                .Add(new ProductInfoHeaderValue("Portfi", "1.0"));

            var gitHubRepositories = await projectService
                .GetGithubProjectsByUsername(username, httpClient);

            return Ok(gitHubRepositories);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentException ex)
        {
            logger.LogError(ex, "Invalid GitHub username: {Username}.", username);

            return BadRequest("Invalid GitHub username provided.");
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Failed to retrieve GitHub projects for username: {Username}.", username);

            return StatusCode(StatusCodes.Status500InternalServerError, "Could not fetch GitHub repositories. Please try again later.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while fetching GitHub projects for username: {Username}.", username);

            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
        }
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
    async public Task<IActionResult> AddProjectDescription(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "description")]
        string description)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to add description to project with ID: {ProjectId}.", projectId);

            MODELS.Project modifiedProject = await projectService
                .AddDescriptionByProjectId(projectId, user.Id, description);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} not found.", projectId);
            return NotFound($"Project with ID {projectId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while adding description to project ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the project description.");
        }
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
    async public Task<IActionResult> AddActiveLinkToProject(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "activeLink")]
        string activeLink)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to add active link to project with ID: {ProjectId}.", projectId);

            MODELS.Project modifiedProject = await projectService
                .AddActiveLinkByProjectId(projectId, user.Id, activeLink);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "An error occurred while adding active link to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while adding active link to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
        }
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
    async public Task<IActionResult> AddCategoriesToProject(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "categories")]
        string[] categories)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
                .GetUser(decodedToken.AccessToken)
                ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to add categories to project with ID: {ProjectId}.", projectId);

            IEnumerable<ENUMS.ProjectCategory> enumValues = categories
                .Select(value => Enum.TryParse(value, out ENUMS.ProjectCategory result)
                    ? result
                    : (ENUMS.ProjectCategory?)null)
                .Where(e => e.HasValue)
                .Select(e => e!.Value);

            MODELS.Project modifiedProject = await projectService
                .AddCategoriesByProjectId(projectId, user.Id, enumValues);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "An error occurred while adding categories to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while adding categories to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
        }
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
    async public Task<IActionResult> EditProjectDescription(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "description")]
        string description)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to edit description to project with ID: {ProjectId}.", projectId);

            MODELS.Project modifiedProject = await projectService
                .EditDescriptionByProjectId(projectId, user.Id, description);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "An error occurred while editting description to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while editting description to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
        }
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
    async public Task<IActionResult> EditActiveLinkToProject(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "activeLink")]
        string activeLink)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to edit active link to project with ID: {ProjectId}.", projectId);

            MODELS.Project modifiedProject = await projectService
                .EditActiveLinkByProjectId(projectId, user.Id, activeLink);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "An error occurred while editting active link to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while editting active link to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
        }
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
    async public Task<IActionResult> EditProjectCategories(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId,
        [Required]
        [FromQuery(Name = "categories")]
        string[] categories)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
                .GetUser(decodedToken.AccessToken)
                ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to edit categories to project with ID: {ProjectId}.", projectId);

            IEnumerable<ENUMS.ProjectCategory> enumValues = categories
                .Select(value => Enum.TryParse(value, out ENUMS.ProjectCategory result)
                    ? result
                    : (ENUMS.ProjectCategory?)null)
                .Where(e => e.HasValue)
                .Select(e => e!.Value);

            MODELS.Project modifiedProject = await projectService
                .EditCategoriesByProjectId(projectId, user.Id, enumValues);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "An error occurred while editting categories to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while editting categories to project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
        }
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
    async public Task<IActionResult> RemoveProjectDescription(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to remove description from project with ID: {ProjectId}.", projectId);

            MODELS.Project modifiedProject = await projectService
                .RemoveDescriptionByProjectId(projectId, user.Id);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "An error occurred while removing description from project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while removing description from project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
        }
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
    async public Task<IActionResult> RemoveActiveLinkFromProject(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to remove active link from project with ID: {ProjectId}.", projectId);

            MODELS.Project modifiedProject = await projectService
                .RemoveActiveLinkByProjectId(projectId, user.Id);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} not found.", projectId);
            return NotFound($"Project with ID {projectId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while removing active link from project ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while removing the active link.");
        }
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
    async public Task<IActionResult> ClearProjectCategories(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Attempting to clear categories from project with ID: {ProjectId}.", projectId);

            MODELS.Project modifiedProject = await projectService
                .RemoveAllCategoriesByProjectId(projectId, user.Id);

            return Ok(modifiedProject);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "An error occurred while clearing categories from project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} couldn't be updated.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project couldn't be updated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred while clearing categories from project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
        }
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
    async public Task<IActionResult> RemoveProjectByProjectId(
        [Required]
        [FromQuery(Name = "projectID")]
        string projectId)
    {
        try
        {
            #region Get User

            DTO.TokenResponse decodedToken;

            try
            {
                decodedToken = Request.Cookies
                    .TryGetDecodedToken();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while decoding cookies.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please contact support.");
            }

            var user = await supabase.Auth
               .GetUser(decodedToken.AccessToken)
               ?? throw new EXCEPTIONS.NotAuthorizedException("User not found.");

            if (string.IsNullOrEmpty(user.Id))
            {
                throw new EXCEPTIONS.NotAuthorizedException("User doesn't have ID.");
            }

            #endregion

            logger.LogInformation("Deleting project with ID: {ProjectId}.", projectId);

            MODELS.Portfolio correspondingPortfolio = await projectService
                .DeleteProjectByProjectId(projectId, user.Id);

            return Ok(correspondingPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action").
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Project with ID {ProjectId} not found.", projectId);
            return NotFound($"Project with ID {projectId} not found.");
        }
        catch (EXCEPTIONS.ItemNotDeletedException ex)
        {
            logger.LogError(ex, "Failed to delete project with ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Project could not be deleted due to an internal constraint.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while deleting project ID: {ProjectId}.", projectId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while deleting the project.");
        }
    }

    #endregion
}