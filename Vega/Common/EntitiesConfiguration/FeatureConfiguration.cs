using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vega.Models;

namespace Vega.Common.EntitiesConfiguration
{
    public class FeatureConfiguration : IEntityTypeConfiguration<FeatureModel>
    {
        public void Configure(EntityTypeBuilder<FeatureModel> builder)
        {
            builder.ToTable("Features");

            builder.HasKey(feature => feature.Id);

            builder.HasMany(feature => feature.Vehicles)
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
