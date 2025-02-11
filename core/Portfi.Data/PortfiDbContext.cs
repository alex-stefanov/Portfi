using Microsoft.EntityFrameworkCore;
using MODELS = Portfi.Data.Models;

namespace Portfi.Data
{
    public class PortfiDbContext
        : DbContext
    {
        #region DbSets

        public DbSet<MODELS.Portfolio> Portfolios { get; set; }

        public DbSet<MODELS.Project> Projects { get; set; }

        public DbSet<MODELS.PortfolioView> PortfolioViews { get; set; }

        public DbSet<MODELS.PortfolioDownload> PortfolioDownloads { get; set; }

        public DbSet<MODELS.SocialMediaLink> SocialMediaLinks { get; set; }

        public DbSet<MODELS.PortfolioLink> PortfolioLinks { get; set; }

        #endregion

        public PortfiDbContext() { }

        public PortfiDbContext(DbContextOptions<PortfiDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("connectionString");
            }
        }

    }
}