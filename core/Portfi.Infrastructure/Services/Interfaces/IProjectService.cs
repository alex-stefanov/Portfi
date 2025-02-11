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
    /// <param name="activeLink">The active link to be added to the project.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> AddActiveLinkByProjectId(
        string projectId,
        string activeLink);

    /// <summary>
    /// Adds categories to a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project to which the categories will be added.</param>
    /// <param name="categories">A collection of categories to be added to the project.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> AddCategoriesByProjectId(
        string projectId,
        IEnumerable<ENUMS.ProjectCategory> categories);

    /// <summary>
    /// Adds a description to a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project to which the description will be added.</param>
    /// <param name="description">The description to be added to the project.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> AddDescriptionByProjectId(
        string projectId,
        string description);

    /// <summary>
    /// Deletes a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project to be deleted.</param>
    /// <returns>The portfolio from where the project was deleted.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotDeletedException">Thrown when item couldn't be deleted.</exception>
    Task<MODELS.Portfolio> DeleteProjectByProjectId(
        string projectId);

    /// <summary>
    /// Edits an active link of a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project whose active link will be edited.</param>
    /// <param name="newActiveLink">The new active link to replace the existing one.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> EditActiveLinkByProjectId(
        string projectId,
        string newActiveLink);

    /// <summary>
    /// Edits categories of a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project whose categories will be edited.</param>
    /// <param name="categories">A collection of categories to replace the existing ones.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> EditCategoriesByProjectId(
        string projectId,
        IEnumerable<ENUMS.ProjectCategory> categories);

    /// <summary>
    /// Edits the description of a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project whose description will be edited.</param>
    /// <param name="newDescription">The new description to replace the existing one.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> EditDescriptionByProjectId(
        string projectId,
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
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> RemoveActiveLinkByProjectId(
        string projectId);

    /// <summary>
    /// Removes the description from a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project from which the description will be removed.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> RemoveDescriptionByProjectId(
        string projectId);

    /// <summary>
    /// Removes all categories from a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project from which the categories will be removed.</param>
    /// <returns>The modified project.</returns>
    /// <exception cref="ArgumentNullException">Thrown when project is null.</exception>
    /// <exception cref="EXCEPTIONS.ItemNotUpdatedException">Thrown when item couldn't be updated.</exception>
    Task<MODELS.Project> RemoveAllCategoriesByProjectId(
        string projectId);
}