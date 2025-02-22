using System.ComponentModel.DataAnnotations;
using Supabase;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Portfi.Common.Helpers;
using MODELS = Portfi.Data.Models;
using DTO = Portfi.Common.Dto;
using EXCEPTIONS = Portfi.Common.Exceptions;
using REQUESTS = Portfi.Infrastructure.Models.Requests;
using RESPONSES = Portfi.Infrastructure.Models.Responses;
using SERVICES = Portfi.Infrastructure.Services.Interfaces;

namespace Portfi.Core.Controllers;

/// <summary>
/// Contains API operations for working with portfolios.
/// </summary>
/// <response code="400">When request malforms.</response>
/// <response code="401">When authentication fails.</response>
/// <response code="500">When an internal server error occurres.</response>
/// <param name="portfolioService">the portfolio service</param>
/// <param name="logger">the logger</param>
[ApiController]
[Route("api/portfolio")]
[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(void))]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RESPONSES.ErrorResponse))]
public class PortfolioController(
    SERVICES.IPortfolioService portfolioService,
    Client supabase,
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
    [HttpGet("ids")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> GetExamplePortfolioIds()
    {
        try
        {
            logger.LogInformation("Attempting to get example portfolios.");

            IEnumerable<MODELS.Portfolio> examplePortfolios = await portfolioService
                .GetExamplePortfolios();

            return Ok(examplePortfolios);
        }
        catch (ArgumentException ex)
        {
            logger.LogError(ex, "Example portfolios couldn't be fetched.");
            return NotFound("Example portfolios couldn't be fetched.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while getting example portfolios.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the example portfolios.");
        }
    }

    /// <summary>
    /// Gets a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that was selected.</returns>
    /// <response code="200">Returns the portfolio that was selected.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpGet("{portfolioId}")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> GetPortfolioById(
        [Required]
        [FromRoute]
        string portfolioId)
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

            logger.LogInformation("Attempting to get portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio foundPortfolio = await portfolioService
                .GetPortfolioById(
                    portfolioId);

            return Ok(foundPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while getting portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting the portfolio.");
        }
    }

    #endregion

    #region POST Requests:

    /// <summary>
    /// Makes a portfolio for a user.
    /// </summary>
    /// <param name="request">the request with bio and names.</param>
    /// <returns>The portfolio that was created.</returns>
    /// <response code="200">Returns the portfolio that was created.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<MODELS.Portfolio>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> CreatePortfolio(
        [Required]
        [FromBody]
        REQUESTS.CreatePortfolioRequest request)
    {
        if (request is null)
        {
            logger.LogError("Request not specified.");
            return BadRequest("Invalid request.");
        }

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

            logger.LogInformation("Attempting to create portfolio with user ID: {UserId}.", user.Id);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .CreatePortfolioByInfo(
                    user.Id,
                    request.Biography,
                    request.Names);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio for user already exists.");
            return NotFound($"Portfolio for user already exists.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while creating portfolio for user.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the portfolio.");
        }
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
    [HttpPost("{portfolioId}/social-media")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> AddSocialMediaLinks(
        [Required]
        [FromRoute]
        string portfolioId,
        [Required]
        [FromBody]
        List<REQUESTS.SocialMediaLinkRequest> socialMediaLinks)
    {
        if (socialMediaLinks is null
            || socialMediaLinks.Count == 0)
        {
            logger.LogError("Social media links cannot be empty.");
            return BadRequest("Social media links cannot be empty.");
        }

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

            logger.LogInformation("Attempting to add social media links for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .AddSocialMediaLinks(
                    portfolioId,
                    user.Id,
                    socialMediaLinks);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while adding social media links for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the social media links.");
        }
    }

    /// <summary>
    /// Adds projects to a portfolio by specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="projects">the projects source codes</param>
    /// <returns>The portfolio that had projects added.</returns>
    /// <response code="200">Returns the portfolio that had projects added to it.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPost("{portfolioId}/projects")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> AddProjects(
        [Required]
        [FromRoute]
        string portfolioId,
        [Required]
        [FromBody]
        List<string> projects)
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

            logger.LogInformation("Attempting to add projects for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .AddProjectsByPortfolioId(
                    portfolioId,
                    user.Id,
                    projects);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while adding projects for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the projects.");
        }
    }

    #endregion

    #region PATCH Requests:

    /// <summary>
    /// Edits a social link from a portfolio by a specified ID.
    /// </summary>
    /// <param name="socialMediaLinkId">the id of the social media link to be editted</param>
    /// <param name="newSocialMediaLink">the new social media link</param>
    /// <returns>The portfolio that had social media link edited.</returns>
    /// <response code="200">Returns the portfolio that had social media link edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("social-media-links/{socialMediaLinkId}")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> EditSocialMediaLink(
        [Required]
        [FromRoute]
        string socialMediaLinkId,
        [Required]
        [FromBody]
        string newSocialMediaLink)
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

            logger.LogInformation("Attempting to edit social media link with ID: {SocialMediaLinkId}.", socialMediaLinkId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .EditSocialMediaLinkById(
                    socialMediaLinkId,
                    user.Id,
                    newSocialMediaLink);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Social media link with ID {SocialMediaLinkId} not found.", socialMediaLinkId);
            return NotFound($"Social media link with ID {socialMediaLinkId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio couldn't be updated");
            return StatusCode(StatusCodes.Status500InternalServerError, "Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while editting social media link with ID: {SocialMediaLinkId}.", socialMediaLinkId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while editting the social media link.");
        }
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
    [HttpPatch("{portfolioId}/biography")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> EditBiography(
        [Required]
        [FromRoute]
        string portfolioId,
        [Required]
        [FromBody]
        string biography)
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

            logger.LogInformation("Attempting to edit biography for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .EditBiographyByPortfolioId(
                    portfolioId,
                    user.Id,
                    biography);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while editting biography for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while editting the biography.");
        }
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
    [HttpPatch("{portfolioId}/theme")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> EditTheme(
        [Required]
        [FromRoute]
        string portfolioId,
        [Required]
        [FromBody]
        REQUESTS.ThemeUpdateRequest request)
    {
        if (request is null)
        {
            logger.LogError("Request not specified.");
            return BadRequest("Invalid request.");
        }

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

            logger.LogInformation("Attempting to edit theme for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .EditThemeByPortfolioId(
                    portfolioId,
                    user.Id,
                    request.BackgroundTheme,
                    request.MainColor);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while editting theme for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while editting the theme.");
        }
    }

    /// <summary>
    /// Sets the background theme and the main color to the default ones from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that had the background theme and the main color set to default.</returns>
    /// <response code="200">Returns the portfolio that had the background theme and the main color set to default.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("{portfolioId}/theme/default")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> SetDefaultTheme(
        [Required]
        [FromRoute]
        string portfolioId)
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

            logger.LogInformation("Attempting to set default theme for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .SetDefaultThemeByportfolioId(
                    portfolioId,
                    user.Id);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while setting defualt theme for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while setting the default theme.");
        }
    }

    /// <summary>
    /// Edits the names from a portfolio by specified project ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="names">the names to be edited</param>
    /// <returns>The portfolio that had the names edited.</returns>
    /// <response code="200">Returns the portfolio that had the names edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("{portfolioId}/names")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> EditNames(
        [Required]
        [FromRoute]
        string portfolioId,
        [Required]
        [FromBody]
        string[] names)
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

            logger.LogInformation("Attempting to edit the person names for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .EditNamesByPortfolioId(
                    portfolioId,
                    user.Id,
                    names);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while editting person names for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while editting the person names.");
        }
    }

    /// <summary>
    /// Edits the visability of a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="isPublic">the visability to be edited</param>
    /// <returns>The portfolio that had the visability edited.</returns>
    /// <response code="200">Returns the portfolio that had the visability edited.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPatch("{portfolioId}/visibility")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> EditVisability(
        [Required]
        [FromRoute]
        string portfolioId,
        [Required]
        [FromBody]
        bool isPublic)
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

            logger.LogInformation("Attempting to edit the visability for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .EditVisabilityByPortfolioId(
                    portfolioId,
                    user.Id,
                    isPublic);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while editting visability for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while editting the visability.");
        }
    }

    #endregion

    #region PUT Requests:

    /// <summary>
    /// Uploads a new avatar url to a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <param name="avatarURL">the avatar url to be added</param>
    /// <returns>The portfolio that had an avatar uploaded.</returns>
    /// <response code="200">Returns the portfolio that had an avatar uploaded.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpPut("{portfolioId}/avatar")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> UploadAvatar(
        [Required]
        [FromRoute]
        string portfolioId,
        [Required]
        [FromBody]
        string avatarURL)
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

            logger.LogInformation("Attempting to upload avatar URL for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .UplaodAvatarByPortfolioId(
                    portfolioId,
                    user.Id,
                    avatarURL);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while uploadding avatar URL for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploadding the avatar URL.");
        }
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
    [HttpPut("{portfolioId}/cv")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> UploadCV(
        [Required]
        [FromRoute]
        string portfolioId,
        [Required]
        [FromBody]
        string cvURL)
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

            logger.LogInformation("Attempting to upload CV URL for portfolio with ID: {PortfolioId}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .UplaodCVByPortfolioId(
                    portfolioId,
                    user.Id,
                    cvURL);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, $"Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while uploadding CV URL for portfolio with ID: {PortfolioId}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploadding the CV URL.");
        }
    }

    #endregion

    #region DELETE Requests:

    /// <summary>
    /// Removes a social links from a portfolio by a specified social media link ID.
    /// </summary>
    /// <param name="socialMediaLinkId">the social media link id to delete</param>
    /// <returns>The portfolio that had social media link removed.</returns>
    /// <response code="200">Returns the portfolio that had social media link removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("socialMediaLinks/{socialMediaLinkId}")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> RemoveSocialMediaLink(
        [Required]
        [FromRoute]
        string socialMediaLinkId)
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

            logger.LogInformation("Attempting to remove social media link with ID: {SocialMediaLinkId}.", socialMediaLinkId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .RemoveSocialMediaLinkById(socialMediaLinkId, user.Id);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Social media link with ID {SocialMediaLinkId} not found.", socialMediaLinkId);
            return NotFound($"Social media link with ID {socialMediaLinkId} not found.");
        }
        catch (EXCEPTIONS.ItemNotDeletedException ex)
        {
            logger.LogError(ex, "Social media link with ID {SocialMediaLink} couldn't be deleted.", socialMediaLinkId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Social media link couldn't be deleted.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while removing social media link with ID: {SocialMediaLinkId}.", socialMediaLinkId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while removing the social media link.");
        }
    }

    /// <summary>
    /// Removes the avatar from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that had the avatar removed.</returns>
    /// <response code="200">Returns the portfolio that had the avatar removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("{portfolioId}/avatar")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> RemoveAvatar(
        [Required]
        [FromRoute]
        string portfolioId)
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

            logger.LogInformation("Attempting to remove avatar from portfolio with ID: {PortfolioID}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .RemoveAvatarByPortfolioId(portfolioId, user.Id);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioID} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while removing avatar from portfolio with ID: {PortfolioID}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while removing the avatar.");
        }
    }

    /// <summary>
    /// Removes the CV from a portfolio by a specified portfolio ID.
    /// </summary>
    /// <param name="portfolioId">the portfolio ID</param>
    /// <returns>The portfolio that had the CV removed.</returns>
    /// <response code="200">Returns the portfolio that had the CV removed.</response>
    /// <response code="401">If the user is unauthorized (invalid or missing JWT).</response>
    /// <response code="500">If there is a server error.</response>
    [HttpDelete("{portfolioId}/cv")]
    [ProducesResponseType(typeof(MODELS.Portfolio), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    async public Task<IActionResult> RemoveCV(
        [Required]
        [FromRoute]
        string portfolioId)
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

            logger.LogInformation("Attempting to remove CV from portfolio with ID: {PortfolioID}.", portfolioId);

            MODELS.Portfolio modifiedPortfolio = await portfolioService
                .RemoveCVByPortfolioId(portfolioId, user.Id);

            return Ok(modifiedPortfolio);
        }
        catch (EXCEPTIONS.NotAuthorizedException ex)
        {
            logger.LogError(ex, "Problem with authorization occured.");

            return StatusCode(StatusCodes.Status401Unauthorized, "Could not authorize for the certian action");
        }
        catch (ArgumentNullException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioID} not found.", portfolioId);
            return NotFound($"Portfolio with ID {portfolioId} not found.");
        }
        catch (EXCEPTIONS.ItemNotUpdatedException ex)
        {
            logger.LogError(ex, "Portfolio with ID {PortfolioId} couldn't be updated.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Portfolio couldn't be updated.");
        }
        catch (FormatException ex)
        {
            logger.LogError(ex, "ID was not in the correct GUID format.");
            return NotFound("ID not in the correct format.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while removing CV from portfolio with ID: {PortfolioID}.", portfolioId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while removing the CV.");
        }
    }

    #endregion
}