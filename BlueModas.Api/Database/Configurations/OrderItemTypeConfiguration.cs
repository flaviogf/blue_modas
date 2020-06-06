using System;
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
                .Property<Guid>("OrderId")
                .IsRequired();

            builder
                .Property<int>("ProductId")
                .IsRequired();

            builder
                .HasKey("OrderId", "ProductId");

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
