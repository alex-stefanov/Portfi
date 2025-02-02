using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MODELS = Portfi.Data.Models;
using REPOSITORIES = Portfi.Data.Repositories;
using SERVICES = Portfi.Infrastructure.Services;

namespace Portfi.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            Type[] typesToExclude = new Type[] { };

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

                    Type[] constructArgs = new Type[2];
                    constructArgs[0] = type;
                    constructArgs[1] = idPropInfo?.PropertyType ?? typeof(object);

                    repositoryInterface = repositoryInterface.MakeGenericType(constructArgs);
                    repositoryInstanceType = repositoryInstanceType.MakeGenericType(constructArgs);

                    services.AddScoped(repositoryInterface, repositoryInstanceType);
                }
            }

            return services;
        }

        public static IServiceCollection RegisterUserDefinedServices(this IServiceCollection services, Assembly serviceAssembly)
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
                    .FirstOrDefault(t => $"I{t.Name}".Equals(serviceInterfaceType.Name, StringComparison.OrdinalIgnoreCase));

                if (serviceType == null)
                {
                    throw new NullReferenceException($"Service type could not be found for {serviceInterfaceType.Name}");
                }

                services.AddScoped(serviceInterfaceType, serviceType);
            }

            return services;
        }
    }
}
