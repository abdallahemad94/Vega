using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Vega.Models;

namespace Vega.Common.EntitiesConfiguration
{
    public class VehicleFeatureConfiguration : IEntityTypeConfiguration<VehicleFeature>
    {
        public void Configure(EntityTypeBuilder<VehicleFeature> builder)
        {
            builder.ToTable("VehiclesFeatures");

            builder.HasKey(vehicleFeature => new { vehicleFeature.VehicleId, vehicleFeature.FeatureId })
                .HasAnnotation("DatabaseGeneratedOption", DatabaseGeneratedOption.Computed);

            builder.HasOne(vehicleFeature => vehicleFeature.Vehicle)
                .WithMany(vehicle => vehicle.Features)
                .HasPrincipalKey(vehicle => vehicle.Id)
                .HasForeignKey(vehicleFeature => vehicleFeature.VehicleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vehicleFeature => vehicleFeature.Feature)
                .WithMany(feature => feature.Vehicles)
                .HasPrincipalKey(feature => feature.Id)
                .HasForeignKey(vehicleFeature => vehicleFeature.FeatureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
