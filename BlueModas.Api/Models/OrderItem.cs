using System;

namespace BlueModas.Api.Models
{
    public class OrderItem
    {
        public Order Order { get; set; }

        public Product Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            return obj is OrderItem orderItem && orderItem.Order.Equals(Order) && orderItem.Product.Equals(Product);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Order, Product);
        }
    }
}
