using Cheeseria.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure.EntityConfigurations
{
    class CheeseEntityTypeConfiguration
        : IEntityTypeConfiguration<Cheese>
    {
        public void Configure(EntityTypeBuilder<Cheese> builder)
        {
            builder.ToTable("Cheese");

            builder.HasKey(c => c.Id);

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cb => cb.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(cb => cb.Colour)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cb => cb.Price)
                .IsRequired();

            builder.Property(cb => cb.ImageUrl);
        }
    }
}
