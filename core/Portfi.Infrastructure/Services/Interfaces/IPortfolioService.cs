using System.Text.Json;
using MODELS = Portfi.Data.Models;
using EXCEPTIONS = Portfi.Common.Exceptions;

namespace Portfi.Infrastructure.Services.Interfaces;

/// <summary>
/// Defines methods for managing portfolios, including editing, retrieving, and updating portfolio details.
/// </summary>
public interface IPortfolioService
{
    /// <summary>
    /// Adds projects to a portfolio by its ID.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="sourceCodeLinks">An array of source code links to be added.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> AddProjectsByPortfolioId(
        string portfolioId,
        string personId,
        string[] sourceCodeLinks);

    /// <summary>
    /// Adds social media links to a portfolio.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="serializedSocialMediaLinks">A serialized representation of the social media links.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the portfolio is null.</exception>
    /// <exception cref="JsonException">Thrown when social media links couldn't be deserialized.</exception>
    /// <exception cref="Exception">Thrown when unexpected error occured.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> AddSocialMediaLinks(
        string portfolioId,
        string personId,
        string serializedSocialMediaLinks);

    /// <summary>
    /// Creates a new portfolio with the provided information.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="biography">The biography of the user.</param>
    /// <param name="names">An array of names associated with the portfolio.</param>
    /// <returns>The newly created portfolio.</returns>
    /// <exception cref="ArgumentException">Thrown when there is a portfolio with the specified user id.</exception>
    Task<MODELS.Portfolio> CreatePortfolioByInfo(
        string userId,
        string biography,
        string[] names);

    /// <summary>
    /// Edits the biography of a portfolio using its ID.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="newBiography">The updated biography.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> EditBiographyByPortfolioId(
        string portfolioId,
        string personId,
        string newBiography);

    /// <summary>
    /// Updates the names associated with a portfolio by its ID.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="newNames">An array of updated names.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> EditNamesByPortfolioId(
        string portfolioId,
        string personId,
        string[] newNames);

    /// <summary>
    /// Updates a specific social media link by its ID.
    /// </summary>
    /// <param name="socialMediaLinkId">The unique identifier of the social media link.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="newSocialMediaLink">The updated social media link.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when socialMediaLink or portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> EditSocialMediaLinkById(
        string socialMediaLinkId,
        string personId,
        string newSocialMediaLink);

    /// <summary>
    /// Edits the theme of a portfolio using its ID.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="newBackgroundTheme">The updated background theme.</param>
    /// <param name="newMainColor">The updated main color.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> EditThemeByPortfolioId(
        string portfolioId,
        string personId,
        string? newBackgroundTheme,
        string? newMainColor);

    /// <summary>
    /// Edits the visability of a portfolio by its ID.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="isPublic">The visability.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> EditVisabilityByPortfolioId(
        string portfolioId,
        string personId,
        bool isPublic);

    /// <summary>
    /// Retrieves a collection of example portfolios.
    /// </summary>
    /// <returns>A list of example portfolios.</returns>
    /// <exception cref="ArgumentException">Thrown when there were no example portfolios found.</exception>
    Task<IEnumerable<MODELS.Portfolio>> GetExamplePortfolios();

    /// <summary>
    /// Retrieves a portfolio by its ID.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <returns>The requested portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> GetPortfolioById(
        string portfolioId);

    /// <summary>
    /// Removes the avatar from a portfolio.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> RemoveAvatarByPortfolioId(
        string portfolioId,
        string personId);

    /// <summary>
    /// Removes the CV from a portfolio.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> RemoveCVByPortfolioId(
        string portfolioId,
        string personId);

    /// <summary>
    /// Removes a social media link by its ID.
    /// </summary>
    /// <param name="socialMediaLinkId">The unique identifier of the social media link.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when socialMediaLink or portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotDeletedException">Thrown when item couldn't be deleted.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> RemoveSocialMediaLinkById(
        string socialMediaLinkId,
        string personId);

    /// <summary>
    /// Sets the default theme for a portfolio by its ID.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> SetDefaultThemeByportfolioId(
        string portfolioId,
        string personId);

    /// <summary>
    /// Uploads an avatar for a portfolio.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="avatarURL">The URL of the avatar image.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> UplaodAvatarByPortfolioId(
        string portfolioId,
        string personId,
        string avatarURL);

    /// <summary>
    /// Uploads a CV for a portfolio.
    /// </summary>
    /// <param name="portfolioId">The unique identifier of the portfolio.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="cvURL">The URL of the CV file.</param>
    /// <returns>The updated portfolio.</returns>
    /// <exception cref="ArgumentNullException">Thrown when portfolio is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the portfolio ID is not in the correct format.</exception>
    Task<MODELS.Portfolio> UplaodCVByPortfolioId(
        string portfolioId,
        string personId,
        string cvURL);
}