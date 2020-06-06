using BlueModas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlueModas.Api.Database.Configurations
{
    public class OrderItemTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
                .ToTable("OrderItems");

            builder
                .HasKey(it => new { it.OrderNumber, it.ProductId });

            builder
                .Property(it => it.OrderNumber)
                .IsRequired();

            builder
                .Property(it => it.ProductId)
                .IsRequired();

            builder
                .HasOne(it => it.Order)
                .WithMany(it => it.Items)
                .IsRequired();

            builder
                .HasOne(it => it.Product)
                .WithMany()
                .IsRequired();

            builder
                .Property(it => it.Price)
                .IsRequired();

            builder
                .Property(it => it.Quantity)
                .IsRequired();
        }
    }
}
