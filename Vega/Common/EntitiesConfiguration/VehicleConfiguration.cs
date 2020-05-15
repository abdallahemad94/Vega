using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vega.Models;

namespace Vega.Common.EntitiesConfiguration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");

            builder.HasKey(vehicle => vehicle.Id);

            builder.HasOne(vehicle => vehicle.Model)
                .WithMany(model => model.Vehicles)
                .HasPrincipalKey(model => model.Id)
                .HasForeignKey(vehicle => vehicle.ModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(vehicle => vehicle.ContactName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(vehicle => vehicle.ContactPhone)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(vehicle => vehicle.ContactEmail)
                .HasMaxLength(255);

            builder.Property(vehicle => vehicle.LastUpdated)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.HasMany(vehicel => vehicel.Features)
                .WithOne(vehicleFeature => vehicleFeature.Vehicle)
                .HasPrincipalKey(vehicle => vehicle.Id)
                .HasForeignKey(vehicleFeature => vehicleFeature.VehicleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(vehicle => vehicle.Photos)
                .WithOne(photo => photo.Vehicle)
                .HasPrincipalKey(vehicle => vehicle.Id)
                .HasForeignKey(photo => photo.VehicleId);
        }
    }
}
