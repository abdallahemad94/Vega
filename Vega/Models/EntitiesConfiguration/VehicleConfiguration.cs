using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models.EntitiesConfiguration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");

            builder.HasKey(vehicle => vehicle.Id);

            builder.HasOne<Model>(vehicle => vehicle.Model)
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

            builder.HasMany<VehicleFeature>(vehicel => vehicel.Features)
                .WithOne(vehicleFeature => vehicleFeature.Vehicle)
                .HasPrincipalKey(vehicle => vehicle.Id)
                .HasForeignKey(VehicleFeature => VehicleFeature.VehicleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
