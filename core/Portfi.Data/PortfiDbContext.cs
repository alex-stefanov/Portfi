using Microsoft.EntityFrameworkCore;
using MODELS = Portfi.Data.Models;

namespace Portfi.Data;

/// <summary>
/// Represents the database context for the Portfi application.
/// </summary>
public class PortfiDbContext : DbContext
{
    #region DbSets

    /// <summary>
    /// Gets or sets the portfolios in the database.
    /// </summary>
    public DbSet<MODELS.Portfolio> Portfolios { get; set; }

    /// <summary>
    /// Gets or sets the projects in the database.
    /// </summary>
    public DbSet<MODELS.Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets the portfolio views in the database.
    /// </summary>
    public DbSet<MODELS.PortfolioView> PortfolioViews { get; set; }

    /// <summary>
    /// Gets or sets the portfolio downloads in the database.
    /// </summary>
    public DbSet<MODELS.PortfolioDownload> PortfolioDownloads { get; set; }

    /// <summary>
    /// Gets or sets the social media links associated with portfolios.
    /// </summary>
    public DbSet<MODELS.SocialMediaLink> SocialMediaLinks { get; set; }

    /// <summary>
    /// Gets or sets the portfolio links in the database.
    /// </summary>
    public DbSet<MODELS.PortfolioLink> PortfolioLinks { get; set; }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="PortfiDbContext"/> class.
    /// </summary>
    public PortfiDbContext() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PortfiDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public PortfiDbContext(
        DbContextOptions<PortfiDbContext> options)
        : base(options) { }

    /// <summary>
    /// Configures the database schema and relationships between entities.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model.</param>
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}