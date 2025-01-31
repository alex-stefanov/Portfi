using System.Text.Json;
using ENUMS = Portfi.Common.Enums;
using MODELS = Portfi.Data.Models;
using REPOSITORIES = Portfi.Data.Repositories;
using INTERFACES = Portfi.Infrastructure.Services.Interfaces;
using RESPONSES = Portfi.Infrastructure.Models.Responses;

namespace Portfi.Infrastructure.Services.Implementations;

public class ProjectService(
    REPOSITORIES.IRepository<MODELS.Project, Guid> repository)
    : INTERFACES.IProjectService
{
    async public Task<bool> AddActiveLinkByProjectId(
        string projectId,
        string activeLink)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        foundProject.HostedLink = activeLink;

        return true;
    }

    async public Task<bool> AddCategoriesByProjectId(
        string projectId,
        IEnumerable<ENUMS.ProjectCategory> categories)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        foundProject.Categories = categories.ToHashSet();

        return true;
    }

    async public Task<bool> AddDescriptionByProjectId(
        string projectId,
        string description)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        foundProject.Description = description;

        return true;
    }

    async public Task<bool> DeleteProjectByProjectId(
        string projectId)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        return await repository.DeleteAsync(foundProject);
    }

    async public Task<bool> EditActiveLinkByProjectId(
        string projectId,
        string newActiveLink)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        if (foundProject.HostedLink != newActiveLink)
        {
            foundProject.HostedLink = newActiveLink;
        }

        return true;

    }

    async public Task<bool> EditCategoriesByProjectId(
        string projectId,
        IEnumerable<ENUMS.ProjectCategory> categories)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        foundProject.Categories ??= [];

        var newCategoriesSet = categories.ToHashSet();

        if (!foundProject.Categories.SetEquals(newCategoriesSet))
        {
            foundProject.Categories.RemoveWhere(c => !newCategoriesSet.Contains(c));

            foundProject.Categories.UnionWith(newCategoriesSet);
        }

        return true;
    }

    async public Task<bool> EditDescriptionByProjectId(
        string projectId,
        string newDescription)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        if (foundProject.Description != newDescription)
        {
            foundProject.Description = newDescription;
        }

        return true;
    }

    async public Task<HashSet<RESPONSES.GitHubRepository>> GetGithubProjectsByUsername(
        string username,
        HttpClient httpClient)
    {
        var endpoint = $"users/{username}/repos";

        try
        {
            var response = await httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var repositories = JsonSerializer.Deserialize<HashSet<RESPONSES.GitHubRepository>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return [..repositories];
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Error fetching GitHub repos: {ex.Message}");
        }
    }

    async public Task<bool> RemoveActiveLinkByProjectId(
        string projectId)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        if (!(string.IsNullOrEmpty(foundProject.HostedLink)
            || string.IsNullOrWhiteSpace(foundProject.HostedLink)))
        {
            foundProject.HostedLink = string.Empty;
        }

        return true;
    }

    async public Task<bool> RemoveAllCategoriesByProjectId(
        string projectId)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        if (foundProject.Categories.Any())
        {
            foundProject.Categories = [];
        }

        return true;
    }

    async public Task<bool> RemoveDescriptionByProjectId(
        string projectId)
    {
        var foundProject = await repository.GetByIdAsync(Guid.Parse(projectId));

        if (foundProject is null)
        {
            return false;
        }

        if (!(string.IsNullOrEmpty(foundProject.Description)
            || string.IsNullOrWhiteSpace(foundProject.Description)))
        {
            foundProject.Description = string.Empty;
        }

        return true;
    }
}