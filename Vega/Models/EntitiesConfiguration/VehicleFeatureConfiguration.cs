using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Vega.Models.EntitiesConfiguration
{
    public class VehicleFeatureConfiguration : IEntityTypeConfiguration<VehicleFeature>
    {
        public void Configure(EntityTypeBuilder<VehicleFeature> builder)
        {
            builder.ToTable("VehiclesFeatures");

            builder.HasKey(vehicleFeature => new { vehicleFeature.VehicleId, vehicleFeature.FeatureId })
                .HasAnnotation("DatabaseGeneratedOption", DatabaseGeneratedOption.Computed);

            builder.HasOne<Vehicle>(vehicleFeature => vehicleFeature.Vehicle)
                .WithMany(vehicle => vehicle.Features)
                .HasPrincipalKey(vehicle => vehicle.Id)
                .HasForeignKey(vehicleFeature => vehicleFeature.VehicleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Feature>(vehicleFeature => vehicleFeature.Feature)
                .WithMany(feature => feature.Vehicles)
                .HasPrincipalKey(feature => feature.Id)
                .HasForeignKey(VehicleFeature => VehicleFeature.FeatureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
