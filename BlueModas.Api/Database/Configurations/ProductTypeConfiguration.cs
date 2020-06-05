using BlueModas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueModas.Api.Database.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("Products");

            builder
                .HasKey(it => it.Id);

            builder
                .Property(it => it.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(it => it.Price)
                .IsRequired();

            builder
                .Property(it => it.Image)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
