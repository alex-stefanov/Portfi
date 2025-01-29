using MODELS = Portfi.Data.Models;
using REPOSITORIES = Portfi.Data.Repositories;
using SERVICES = Portfi.Infrastructure.Services;

namespace Portfi.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<REPOSITORIES.IRepository<MODELS.Portfolio, Guid>, REPOSITORIES.Repository<MODELS.Portfolio, Guid>>();
        services.AddScoped<REPOSITORIES.IRepository<MODELS.PortfolioDownload, Guid>, REPOSITORIES.Repository<MODELS.PortfolioDownload, Guid>>();
        services.AddScoped<REPOSITORIES.IRepository<MODELS.PortfolioView, Guid>, REPOSITORIES.Repository<MODELS.PortfolioView, Guid>>();
        services.AddScoped<REPOSITORIES.IRepository<MODELS.Project, Guid>, REPOSITORIES.Repository<MODELS.Project, Guid>>();

        return services;
    }

    public static IServiceCollection RegisterUserDefinedServices(
        this IServiceCollection services)
    {
        services.AddScoped<SERVICES.Interfaces.IPortfolioService, SERVICES.Implementations.PortfolioService>();
        services.AddScoped<SERVICES.Interfaces.IProjectService, SERVICES.Implementations.ProjectService>();

        return services;
    }
}
