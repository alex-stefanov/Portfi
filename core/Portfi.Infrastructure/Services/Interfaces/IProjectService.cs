using MODELS = Portfi.Data.Models;
using ENUMS = Portfi.Common.Enums;
using RESPONSES = Portfi.Infrastructure.Models.Responses;
using EXCEPTIONS = Portfi.Common.Exceptions;

namespace Portfi.Infrastructure.Services.Interfaces;

/// <summary>
/// Defines methods for managing project-related data.
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Adds an active link to a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project to which the active link will be added.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="activeLink">The active link to be added to the project.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception>
    Task<MODELS.Project> AddActiveLinkByProjectId(
        string projectId,
        string personId,
        string activeLink);

    /// <summary>
    /// Adds categories to a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project to which the categories will be added.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="categories">A collection of categories to be added to the project.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception>
    Task<MODELS.Project> AddCategoriesByProjectId(
        string projectId,
        string personId,
        IEnumerable<ENUMS.ProjectCategory> categories);

    /// <summary>
    /// Adds a description to a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project to which the description will be added.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="description">The description to be added to the project.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception></exception>
    Task<MODELS.Project> AddDescriptionByProjectId(
        string projectId,
        string personId,
        string description);

    /// <summary>
    /// Deletes a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project to be deleted.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <returns>The portfolio from where the project was deleted.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotDeletedException">Thrown when item couldn't be deleted.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception></exception>
    Task<MODELS.Portfolio> DeleteProjectByProjectId(
        string projectId,
        string personId);

    /// <summary>
    /// Edits an active link of a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project whose active link will be edited.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="newActiveLink">The new active link to replace the existing one.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception></exception>"
    Task<MODELS.Project> EditActiveLinkByProjectId(
        string projectId,
        string personId,
        string newActiveLink);

    /// <summary>
    /// Edits categories of a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project whose categories will be edited.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="categories">A collection of categories to replace the existing ones.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception>
    Task<MODELS.Project> EditCategoriesByProjectId(
        string projectId,
        string personId,
        IEnumerable<ENUMS.ProjectCategory> categories);

    /// <summary>
    /// Edits the description of a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project whose description will be edited.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <param name="newDescription">The new description to replace the existing one.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception>
    Task<MODELS.Project> EditDescriptionByProjectId(
        string projectId,
        string personId,
        string newDescription);

    /// <summary>
    /// Gets GitHub repositories for a specific username.
    /// </summary>
    /// <param name="username">The GitHub username whose repositories will be fetched.</param>
    /// <param name="httpClient">An HTTP client used to send requests to GitHub.</param>
    /// <returns>A collection of GitHub repositories associated with the username.</returns>
    /// <exception cref="Exception">Thrown when an unexpected error in the fetching occured.</exception>
    Task<HashSet<RESPONSES.GitHubRepository>> GetGithubProjectsByUsername(
        string username,
        HttpClient httpClient);

    /// <summary>
    /// Removes an active link from a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project from which the active link will be removed.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception>"
    Task<MODELS.Project> RemoveActiveLinkByProjectId(
        string projectId,
        string personId);

    /// <summary>
    /// Removes the description from a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project from which the description will be removed.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception>
    Task<MODELS.Project> RemoveDescriptionByProjectId(
        string projectId,
        string personId);

    /// <summary>
    /// Removes all categories from a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project from which the categories will be removed.</param>
    /// <param name="personId">The unique identifier of the person for authorization.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    /// <exception cref="EXCEPTIONS.NotAuthorizedException">Thrown when user doesn't have the required permission.</exception>
    /// <exception cref="FormatException">Thrown when the project ID is not in the correct format.</exception>
    Task<MODELS.Project> RemoveAllCategoriesByProjectId(
        string projectId,
        string personId);
}