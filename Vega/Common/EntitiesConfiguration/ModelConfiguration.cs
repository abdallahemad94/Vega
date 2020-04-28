using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vega.Models;

namespace Vega.Common.EntitiesConfiguration
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Models");

            builder.HasKey(model => model.Id);

            builder.Property(model => model.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(model => model.Make)
                .WithMany(make => make.Models)
                .HasPrincipalKey(make => make.Id)
                .HasForeignKey(model => model.MakeId)
                .IsRequired();

            builder.HasMany(model => model.Vehicles)
                .WithOne(vehicle => vehicle.Model)
                .HasPrincipalKey(model => model.Id)
                .HasForeignKey(vehicle => vehicle.ModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
