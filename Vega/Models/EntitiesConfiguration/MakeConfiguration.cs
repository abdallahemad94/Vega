using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.Models.EntitiesConfiguration
{
    public class MakeConfiguration : IEntityTypeConfiguration<Make>
    {
        public void Configure(EntityTypeBuilder<Make> builder)
        {
            builder.ToTable("Makes");

            builder.HasKey(make => make.Id);

            builder.HasMany<Model>(make => make.Models)
                .WithOne(model => model.Make)
                .HasPrincipalKey(make => make.Id)
                .HasForeignKey(model => model.MakeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(make => make.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
