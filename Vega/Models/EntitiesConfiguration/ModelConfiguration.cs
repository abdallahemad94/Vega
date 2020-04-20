using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models.EntitiesConfiguration
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

            builder.HasOne<Make>(model => model.Make)
                .WithMany(make => make.Models)
                .HasPrincipalKey(make => make.Id)
                .HasForeignKey(model => model.MakeId)
                .IsRequired();

            builder.HasMany<Vehicle>(model => model.Vehicles)
                .WithOne(vehicle => vehicle.Model)
                .HasPrincipalKey(model => model.Id)
                .HasForeignKey(vehicle => vehicle.ModelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
