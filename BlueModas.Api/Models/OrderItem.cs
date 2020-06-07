using System;

namespace BlueModas.Api.Models
{
    public class OrderItem
    {
        public Guid OrderNumber { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal => Price * Quantity;

        public override bool Equals(object obj)
        {
            return obj is OrderItem orderItem && orderItem.OrderNumber == OrderNumber && orderItem.ProductId == ProductId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderNumber, ProductId);
        }
    }
}
