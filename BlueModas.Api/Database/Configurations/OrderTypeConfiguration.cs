using BlueModas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueModas.Api.Database.Configurations
{
    public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .ToTable("Orders");

            builder
                .HasKey(it => it.Number);

            builder
                .OwnsOne(it => it.Customer);

            builder
                .HasMany(it => it.Items)
                .WithOne()
                .IsRequired();
        }
    }
}
