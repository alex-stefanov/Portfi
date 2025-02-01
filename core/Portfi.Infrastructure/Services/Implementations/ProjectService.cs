using System.Text.Json;
using ENUMS = Portfi.Common.Enums;
using MODELS = Portfi.Data.Models;
using REPOSITORIES = Portfi.Data.Repositories;
using INTERFACES = Portfi.Infrastructure.Services.Interfaces;
using RESPONSES = Portfi.Infrastructure.Models.Responses;

namespace Portfi.Infrastructure.Services.Implementations;

/// <inheritdoc/>
public class ProjectService(
    REPOSITORIES.IRepository<MODELS.Project, Guid> projectRepository,
        REPOSITORIES.IRepository<MODELS.Portfolio, Guid> portfolioRepository)
    : INTERFACES.IProjectService
{
    /// <inheritdoc/>
    async public Task<MODELS.Project> AddActiveLinkByProjectId(
        string projectId,
        string activeLink)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId)) 
            ?? throw new ArgumentNullException("Project not found");

        foundProject.HostedLink = activeLink;

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> AddCategoriesByProjectId(
        string projectId,
        IEnumerable<ENUMS.ProjectCategory> categories)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
           ?? throw new ArgumentNullException("Project not found");

        foundProject.Categories = categories.ToHashSet();

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> AddDescriptionByProjectId(
        string projectId,
        string description)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
           ?? throw new ArgumentNullException("Project not found");

        foundProject.Description = description;

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> DeleteProjectByProjectId(
        string projectId)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
           ?? throw new ArgumentNullException("Project not found");

        Guid portfolioId = foundProject.PortfolioId;

        if(!await projectRepository.DeleteAsync(foundProject))
        {
            throw new ArgumentNullException($"{nameof(projectRepository)} cannot be deleted");
        }

        var foundPortfolio = await portfolioRepository.GetByIdAsync(portfolioId)
            ?? throw new ArgumentNullException("Portfolio not found");

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> EditActiveLinkByProjectId(
        string projectId,
        string newActiveLink)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
            ?? throw new ArgumentNullException("Project not found");

        if (foundProject.HostedLink != newActiveLink)
        {
            foundProject.HostedLink = newActiveLink;
        }

        return foundProject;

    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> EditCategoriesByProjectId(
        string projectId,
        IEnumerable<ENUMS.ProjectCategory> categories)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
            ?? throw new ArgumentNullException("Project not found");

        foundProject.Categories ??= [];

        var newCategoriesSet = categories.ToHashSet();

        if (!foundProject.Categories.SetEquals(newCategoriesSet))
        {
            foundProject.Categories.RemoveWhere(c => !newCategoriesSet.Contains(c));

            foundProject.Categories.UnionWith(newCategoriesSet);
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> EditDescriptionByProjectId(
        string projectId,
        string newDescription)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
             ?? throw new ArgumentNullException("Project not found");

        if (foundProject.Description != newDescription)
        {
            foundProject.Description = newDescription;
        }

        return foundProject;
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    async public Task<MODELS.Project> RemoveActiveLinkByProjectId(
        string projectId)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
             ?? throw new ArgumentNullException("Project not found");

        if (!(string.IsNullOrEmpty(foundProject.HostedLink)
            || string.IsNullOrWhiteSpace(foundProject.HostedLink)))
        {
            foundProject.HostedLink = string.Empty;
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> RemoveAllCategoriesByProjectId(
        string projectId)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
             ?? throw new ArgumentNullException("Project not found");

        if (foundProject.Categories.Any())
        {
            foundProject.Categories = [];
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> RemoveDescriptionByProjectId(
        string projectId)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
             ?? throw new ArgumentNullException("Project not found");

        if (!(string.IsNullOrEmpty(foundProject.Description)
            || string.IsNullOrWhiteSpace(foundProject.Description)))
        {
            foundProject.Description = string.Empty;
        }

        return foundProject;
    }
}