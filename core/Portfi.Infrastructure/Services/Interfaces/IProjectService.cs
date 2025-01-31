using ENUMS = Portfi.Common.Enums;
using RESPONSES = Portfi.Infrastructure.Models.Responses;

namespace Portfi.Infrastructure.Services.Interfaces;

public interface IProjectService
{
    Task<bool> AddActiveLinkByProjectId(
        string projectId,
        string activeLink);

    Task<bool> AddCategoriesByProjectId(
        string projectId,
        IEnumerable<ENUMS.ProjectCategory> categories);

    Task<bool> AddDescriptionByProjectId
        (string projectId,
        string description);

    Task<bool> DeleteProjectByProjectId(
        string projectId);

    Task<bool> EditActiveLinkByProjectId(
        string projectId,
        string newActiveLink);

    Task<bool> EditCategoriesByProjectId(
        string projectId,
        IEnumerable<ENUMS.ProjectCategory> categories);

    Task<bool> EditDescriptionByProjectId(
        string projectId,
        string newDescription);

    Task<HashSet<RESPONSES.GitHubRepository>> GetGithubProjectsByUsername(
        string username,
        HttpClient httpClient);

    Task<bool> RemoveActiveLinkByProjectId(
        string projectId);

    Task<bool> RemoveDescriptionByProjectId(
        string projectId);

    Task<bool> RemoveAllCategoriesByProjectId(
        string  projectId);
}