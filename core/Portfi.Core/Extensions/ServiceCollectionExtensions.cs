using System.Reflection;
using REPOSITORIES = Portfi.Data.Repositories;

namespace Portfi.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterRepositories(
        this IServiceCollection services,
        Assembly modelsAssembly)
    {
        Type[] typesToExclude = [];

        Type[] modelTypes = modelsAssembly
            .GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .ToArray();

        foreach (Type type in modelTypes)
        {
            if (!typesToExclude.Contains(type))
            {
                Type repositoryInterface = typeof(REPOSITORIES.IRepository<,>);
                Type repositoryInstanceType = typeof(REPOSITORIES.Repository<,>);

                PropertyInfo? idPropInfo = type
                    .GetProperties()
                    .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));

                Type[] constructArgs = [type, idPropInfo?.PropertyType ?? typeof(object)];
                repositoryInterface = repositoryInterface.MakeGenericType(constructArgs);
                repositoryInstanceType = repositoryInstanceType.MakeGenericType(constructArgs);

                services.AddScoped(repositoryInterface, repositoryInstanceType);
            }
        }

        return services;
    }

    public static IServiceCollection RegisterUserDefinedServices(
        this IServiceCollection services,
        Assembly serviceAssembly)
    {
        Type[] serviceInterfaceTypes = serviceAssembly
            .GetTypes()
            .Where(t => t.IsInterface && t.Namespace?.StartsWith("Portfi.Infrastructure.Services.Interfaces") == true)
            .ToArray();

        Type[] serviceTypes = serviceAssembly
            .GetTypes()
            .Where(t => !t.IsInterface && !t.IsAbstract &&
                        t.Namespace?.StartsWith("Portfi.Infrastructure.Services.Implementations") == true)
            .ToArray();

        foreach (Type serviceInterfaceType in serviceInterfaceTypes)
        {
            Type? serviceType = serviceTypes
                .FirstOrDefault(t => $"I{t.Name}".Equals(serviceInterfaceType.Name, StringComparison.OrdinalIgnoreCase)) 
                ?? throw new NullReferenceException($"Service type could not be found for {serviceInterfaceType.Name}");

            services.AddScoped(serviceInterfaceType, serviceType);
        }

        return services;
    }
}
