namespace Portfi.Core.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IConfigurationBuilder"/>.
/// </summary>
public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// Adds environment-specific JSON configuration files to the configuration builder.
    /// </summary>
    /// <param name="builder">The <see cref="IConfigurationBuilder"/> instance to configure.</param>
    /// <param name="environment">The <see cref="IWebHostEnvironment"/> representing the hosting environment.</param>
    /// <param name="connectionString">Outputs the connection string based on the environment.</param>
    /// <returns>The updated <see cref="IConfigurationBuilder"/> instance.</returns>
    public static IConfigurationBuilder AddEnvironmentSpecificJsonFiles(
        this IConfigurationBuilder builder,
        IWebHostEnvironment environment,
        out string connectionString)
    {
        builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        if (environment.EnvironmentName == "Development")
        {
            builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            connectionString = builder.Build().GetConnectionString("DevConnection")!;
        }
        else
        {
            builder.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
            connectionString = builder.Build().GetConnectionString("ProdConnection")!;
        }

        return builder;
    }
}
