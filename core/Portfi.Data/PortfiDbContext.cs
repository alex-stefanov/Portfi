using Microsoft.EntityFrameworkCore;
using MODELS = Portfi.Data.Models;

namespace Portfi.Data
{
    public class PortfiDbContext(DbContextOptions<PortfiDbContext> options)
        : DbContext(options)
    {
        #region DbSets

        public DbSet<MODELS.Portfolio> Portfolios { get; set; }

        public DbSet<MODELS.Project> Projects { get; set; }

        public DbSet<MODELS.PortfolioView> PortfolioViews { get; set; }

        public DbSet<MODELS.PortfolioDownload> PortfolioDownloads { get; set; }

        public DbSet<MODELS.SocialMediaLink> SocialMediaLinks { get; set; }

        public DbSet<MODELS.PortfolioLink> PortfolioLinks { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MODELS.Portfolio>()
                .HasMany(p => p.Projects)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MODELS.Portfolio>()
                .HasMany(p => p.PortfolioViews)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MODELS.Portfolio>()
                .HasMany(p => p.PortfolioDownloads)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MODELS.Portfolio>()
                .HasMany(p => p.SocialMediaLinks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MODELS.Portfolio>()
                .HasMany(p => p.PortfolioLinks)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}