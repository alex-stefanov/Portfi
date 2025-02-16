using System.Text.Json;
using ENUMS = Portfi.Common.Enums;
using MODELS = Portfi.Data.Models;
using EXCEPTIONS = Portfi.Common.Exceptions;
using REPOSITORIES = Portfi.Data.Repositories;
using RESPONSES = Portfi.Infrastructure.Models.Responses;
using INTERFACES = Portfi.Infrastructure.Services.Interfaces;

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
        string personId,
        string activeLink)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
            ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        foundProject.HostedLink = activeLink;

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> AddCategoriesByProjectId(
        string projectId,
         string personId,
        IEnumerable<ENUMS.ProjectCategory> categories)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
           ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        foundProject.Categories = categories.ToList();

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> AddDescriptionByProjectId(
        string projectId,
        string personId,
        string description)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
           ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        foundProject.Description = description;

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> DeleteProjectByProjectId(
        string projectId,
        string personId)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
           ?? throw new ArgumentNullException("Project not found.");

        Guid portfolioId = foundProject.PortfolioId;

        var foundPortfolio = await portfolioRepository.GetByIdAsync(portfolioId)
            ?? throw new ArgumentNullException("Portfolio not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        if (!await projectRepository.DeleteAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotDeletedException($"{nameof(projectRepository)} cannot be deleted.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> EditActiveLinkByProjectId(
        string projectId,
        string personId,
        string newActiveLink)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
            ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        if (foundProject.HostedLink != newActiveLink)
        {
            foundProject.HostedLink = newActiveLink;
        }

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
        }

        return foundProject;

    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> EditCategoriesByProjectId(
        string projectId,
        string personId,
        IEnumerable<ENUMS.ProjectCategory> categories)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
            ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        foundProject.Categories ??= [];

        if (!foundProject.Categories.SequenceEqual(categories))
        {
            foundProject.Categories.Clear();
            foundProject.Categories.AddRange(categories);
        }

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> EditDescriptionByProjectId(
        string projectId,
        string personId,
        string newDescription)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
             ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        if (foundProject.Description != newDescription)
        {
            foundProject.Description = newDescription;
        }

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
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

            return [.. repositories];
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Error fetching GitHub repos. {ex.Message}");
        }
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> RemoveActiveLinkByProjectId(
        string projectId,
        string personId)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
             ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        if (!(string.IsNullOrEmpty(foundProject.HostedLink)
            || string.IsNullOrWhiteSpace(foundProject.HostedLink)))
        {
            foundProject.HostedLink = null;
        }

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> RemoveAllCategoriesByProjectId(
        string projectId,
        string personId)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
             ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        if (foundProject.Categories.Any())
        {
            foundProject.Categories = [];
        }

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
        }

        return foundProject;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Project> RemoveDescriptionByProjectId(
        string projectId,
        string personId)
    {
        var foundProject = await projectRepository.GetByIdAsync(Guid.Parse(projectId))
             ?? throw new ArgumentNullException("Project not found.");

        await AuthorizeByPortfolioId(foundProject.PortfolioId, personId);

        if (!(string.IsNullOrEmpty(foundProject.Description)
            || string.IsNullOrWhiteSpace(foundProject.Description)))
        {
            foundProject.Description = null;
        }

        if (!await projectRepository.UpdateAsync(foundProject))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundProject)} cannot be updated.");
        }

        return foundProject;
    }

    async public Task AuthorizeByPortfolioId(
        Guid portfolioId,
        string personId)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(portfolioId)
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }
    }
}