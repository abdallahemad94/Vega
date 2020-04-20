using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models.EntitiesConfiguration
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features");

            builder.HasKey(feature => feature.Id);

            builder.HasMany<VehicleFeature>(feature => feature.Vehicles)
                .WithOne(vehicleFeature => vehicleFeature.Feature)
                .HasPrincipalKey(feature => feature.Id)
                .HasForeignKey(vehicleFeautre => vehicleFeautre.FeatureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(feature => feature.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
