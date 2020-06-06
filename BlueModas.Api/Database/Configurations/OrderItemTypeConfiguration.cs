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
                .HasKey(it => it.Id);

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
