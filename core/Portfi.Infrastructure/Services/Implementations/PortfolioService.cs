using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MODELS = Portfi.Data.Models;
using EXCEPTIONS = Portfi.Common.Exceptions;
using REPOSITORIES = Portfi.Data.Repositories;
using REQUESTS = Portfi.Infrastructure.Models.Requests;
using INTERFACES = Portfi.Infrastructure.Services.Interfaces;
using PCONST = Portfi.Common.Constants.PortfolioConstants;
using GCONST = Portfi.Common.Constants.GeneralConstants;

namespace Portfi.Infrastructure.Services.Implementations;

/// <inheritdoc/>
public class PortfolioService(
    REPOSITORIES.IRepository<MODELS.Project, Guid> projectRepository,
    REPOSITORIES.IRepository<MODELS.Portfolio, Guid> portfolioRepository,
    REPOSITORIES.IRepository<MODELS.SocialMediaLink, Guid> socialMediaLinkRepository)
    : INTERFACES.IPortfolioService
{
    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> AddProjectsByPortfolioId(
        string portfolioId,
        string[] sourceCodeLinks)
    {
        var foundPortfolio = await portfolioRepository
            .GetAllAttached()
            .Include(p => p.Projects)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        foreach (string sourceCodeLink in sourceCodeLinks)
        {
            if (foundPortfolio.Projects
                .Any(p => p.SourceCodeLink == sourceCodeLink))
            {
                continue;
            }

            var newProject = new MODELS.Project
            {
                PortfolioId = foundPortfolio.Id,
                SourceCodeLink = sourceCodeLink,
            };

            await projectRepository.AddAsync(newProject);
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> AddSocialMediaLinks(
        string portfolioId,
        string serializedSocialMediaLinks)
    {
        IEnumerable<REQUESTS.AddSocialMediaLinkRequest> socialMediaLinkRequests = [];

        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            socialMediaLinkRequests = JsonSerializer
                .Deserialize<IEnumerable<REQUESTS.AddSocialMediaLinkRequest>>(
                serializedSocialMediaLinks,
                options)
                ?? [];
        }
        catch (JsonException ex)
        {
            throw new JsonException("Invalid JSON format.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error occured.", ex);
        }

        var foundPortfolio = await portfolioRepository
            .GetAllAttached()
            .Include(p => p.SocialMediaLinks)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");


        foreach (REQUESTS.AddSocialMediaLinkRequest socialMediaLink in socialMediaLinkRequests)
        {
            if (foundPortfolio.SocialMediaLinks
                .Any(link => link.Type == socialMediaLink.Type
                    && link.Value == socialMediaLink.Value))
            {
                continue;
            }

            var newSocialMediaLink = new MODELS.SocialMediaLink
            {
                PortfolioId = foundPortfolio.Id,
                Type = socialMediaLink.Type,
                Value = socialMediaLink.Value
            };

            await socialMediaLinkRepository.AddAsync(newSocialMediaLink);
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> CreatePortfolioByInfo(
        string userId,
        string biography,
        string[] names)
    {
        var foundPortfolio = await portfolioRepository.FirstOrDefaultAsync(
            p => p.PersonId == userId);

        if (foundPortfolio is not null)
        {
            throw new ArgumentException($"Portfolio for user with id `{userId}` already exists.");
        }

        var newPortfolio = new MODELS.Portfolio
        {
            PersonId = userId,
            Biography = biography,
            PersonNames = names
        };

        await portfolioRepository.AddAsync(newPortfolio);

        return newPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> EditBiographyByPortfolioId(
        string portfolioId,
        string newBiography)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.Biography != newBiography)
        {
            foundPortfolio.Biography = newBiography;
        }

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> EditNamesByPortfolioId(
        string portfolioId,
        string[] newNames)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        foundPortfolio.PersonNames = newNames;

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> EditSocialMediaLinkById(
        string socialMediaLinkId,
        string newSocialMediaLink)
    {
        var foundSocialMediaLink = await socialMediaLinkRepository.GetByIdAsync(Guid.Parse(socialMediaLinkId))
            ?? throw new ArgumentNullException("Social media link not found.");

        Guid portfolioId = foundSocialMediaLink.PortfolioId;

        if (foundSocialMediaLink.Value != newSocialMediaLink)
        {
            foundSocialMediaLink.Value = newSocialMediaLink;

            if (!await socialMediaLinkRepository.UpdateAsync(foundSocialMediaLink))
            {
                throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundSocialMediaLink)} cannot be updated.");
            }
        }

        var foundPortfolio = await portfolioRepository.GetByIdAsync(portfolioId)
            ?? throw new ArgumentNullException("Portfolio not found.");

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> EditThemeByPortfolioId(
        string portfolioId,
        string? newBackgroundTheme,
        string? newMainColor)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (!string.IsNullOrEmpty(newBackgroundTheme)
            && foundPortfolio.BackgroundTheme != newBackgroundTheme)
        {
            foundPortfolio.BackgroundTheme = newBackgroundTheme;
        }

        if (!string.IsNullOrEmpty(newMainColor)
            && foundPortfolio.MainColor != newMainColor)
        {
            foundPortfolio.MainColor = newMainColor;
        }

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> EditVisabilityByPortfolioId(
        string portfolioId,
        bool isPublic)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.IsPublic != isPublic)
        {
            foundPortfolio.IsPublic = isPublic;
        }

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<IEnumerable<MODELS.Portfolio>> GetExamplePortfolios()
    {
        var examplePortfolios = await portfolioRepository
            .GetAllAttached()
            .Where(p => GCONST.IdsOfExamplePortfolioHolders.Contains(p.PersonId))
            .ToListAsync();

        if (examplePortfolios.Count == 0)
        {
            throw new ArgumentException("No example portfolios found.");
        }

        return examplePortfolios;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> GetPortfolioById(
        string portfolioId)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
           ?? throw new ArgumentNullException("Portfolio not found.");

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> RemoveAvatarByPortfolioId(
        string portfolioId)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        foundPortfolio.Avatar = PCONST.DefaultAvatarValue;

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> RemoveCVByPortfolioId(
        string portfolioId)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        foundPortfolio.CV = null;

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;

    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> RemoveSocialMediaLinkById(
        string socialMediaLinkId)
    {
        var foundSocialMediaLink = await socialMediaLinkRepository.GetByIdAsync(Guid.Parse(socialMediaLinkId))
            ?? throw new ArgumentNullException("Social media link not found.");

        Guid portfolioId = foundSocialMediaLink.PortfolioId;

        if (!await socialMediaLinkRepository.DeleteAsync(foundSocialMediaLink))
        {
            throw new EXCEPTIONS.ItemNotDeletedException($"{nameof(foundSocialMediaLink)} cannot be deleted.");
        }

        var foundPortfolio = await portfolioRepository.GetByIdAsync(portfolioId)
            ?? throw new ArgumentNullException("Portfolio not found.");

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> SetDefaultThemeByportfolioId(
        string portfolioId)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        bool isUpdated = false;

        if (foundPortfolio.BackgroundTheme != PCONST.DefaultBackgroundThemeValue)
        {
            foundPortfolio.BackgroundTheme = PCONST.DefaultBackgroundThemeValue;

            isUpdated = true;
        }

        if (foundPortfolio.MainColor != PCONST.DefaultMainColorValue)
        {
            foundPortfolio.MainColor = PCONST.DefaultMainColorValue;

            isUpdated = true;
        }

        if (isUpdated)
        {
            if (!await portfolioRepository.UpdateAsync(foundPortfolio))
            {
                throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
            }
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> UplaodAvatarByPortfolioId(
        string portfolioId,
        string avatarURL)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.Avatar != avatarURL)
        {
            foundPortfolio.Avatar = avatarURL;
        }

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> UplaodCVByPortfolioId(
        string portfolioId,
        string cvURL)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.CV != cvURL)
        {
            foundPortfolio.CV = cvURL;
        }

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;
    }
}