using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vega.Models;

namespace Vega.Common.EntitiesConfiguration
{
    public class VehiclePhotoConfiguration : IEntityTypeConfiguration<VehiclePhoto>
    {
        public void Configure(EntityTypeBuilder<VehiclePhoto> builder)
        {
            builder.ToTable("VehiclesPhotos");
            builder.HasKey(photos => photos.Id);
            builder.HasOne(photo => photo.Vehicle)
                .WithMany(vehicle => vehicle.Photos)
                .HasPrincipalKey(vehicle => vehicle.Id)
                .HasForeignKey(photo => photo.VehicleId);
        }
    }
}