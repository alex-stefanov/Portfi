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
        string personId,
        IEnumerable<string> sourceCodeLinks)
    {
        var foundPortfolio = await portfolioRepository
            .GetAllAttached()
            .Include(p => p.Projects)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

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
        string personId,
        List<REQUESTS.SocialMediaLinkRequest> socialMediaLinks)
    {
        var foundPortfolio = await portfolioRepository
            .GetAllAttached()
            .Include(p => p.SocialMediaLinks)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

        foreach (REQUESTS.SocialMediaLinkRequest socialMediaLink in socialMediaLinks)
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
        string personId,
        string newBiography)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

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
        string personId,
        string[] newNames)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

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
        string personId,
        string newSocialMediaLink)
    {
        var foundSocialMediaLink = await socialMediaLinkRepository.GetByIdAsync(Guid.Parse(socialMediaLinkId))
            ?? throw new ArgumentNullException("Social media link not found.");

        Guid portfolioId = foundSocialMediaLink.PortfolioId;

        var foundPortfolio = await portfolioRepository.GetByIdAsync(portfolioId)
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

        if (foundSocialMediaLink.Value != newSocialMediaLink)
        {
            foundSocialMediaLink.Value = newSocialMediaLink;

            if (!await socialMediaLinkRepository.UpdateAsync(foundSocialMediaLink))
            {
                throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundSocialMediaLink)} cannot be updated.");
            }
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> EditThemeByPortfolioId(
        string portfolioId,
        string personId,
        string? newBackgroundTheme,
        string? newMainColor)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

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
        string personId,
        bool isPublic)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

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
        string portfolioId,
        string personId)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

        foundPortfolio.Avatar = PCONST.DefaultAvatarValue;

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> RemoveCVByPortfolioId(
        string portfolioId,
        string personId)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

        foundPortfolio.CV = null;

        if (!await portfolioRepository.UpdateAsync(foundPortfolio))
        {
            throw new EXCEPTIONS.ItemNotUpdatedException($"{nameof(foundPortfolio)} cannot be updated.");
        }

        return foundPortfolio;

    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> RemoveSocialMediaLinkById(
        string socialMediaLinkId,
        string personId)
    {
        var foundSocialMediaLink = await socialMediaLinkRepository.GetByIdAsync(Guid.Parse(socialMediaLinkId))
            ?? throw new ArgumentNullException("Social media link not found.");

        Guid portfolioId = foundSocialMediaLink.PortfolioId;

        var foundPortfolio = await portfolioRepository.GetByIdAsync(portfolioId)
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

        if (!await socialMediaLinkRepository.DeleteAsync(foundSocialMediaLink))
        {
            throw new EXCEPTIONS.ItemNotDeletedException($"{nameof(foundSocialMediaLink)} cannot be deleted.");
        }

        return foundPortfolio;
    }

    /// <inheritdoc/>
    async public Task<MODELS.Portfolio> SetDefaultThemeByportfolioId(
        string portfolioId,
        string personId)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

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
        string personId,
        string avatarURL)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

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
        string personId,
        string cvURL)
    {
        var foundPortfolio = await portfolioRepository.GetByIdAsync(Guid.Parse(portfolioId))
            ?? throw new ArgumentNullException("Portfolio not found.");

        if (foundPortfolio.PersonId != personId)
        {
            throw new EXCEPTIONS.NotAuthorizedException($"No permission for user with ID `{personId}`.");
        }

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