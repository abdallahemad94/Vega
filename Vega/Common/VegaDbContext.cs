using Microsoft.EntityFrameworkCore;
using Vega.Common.EntitiesConfiguration;
using Vega.Models;

namespace Vega.Common
{
    public sealed class VegaDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<FeatureModel> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }

        public VegaDbContext(DbContextOptions options) : base(options) 
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MakeConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfiguration());
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleFeatureConfiguration());
        }

    }
}
