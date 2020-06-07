using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueModas.Api.Models
{
    public class Order
    {
        public Guid Number { get; set; }

        public Customer Customer { get; set; }

        public ISet<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal Total => Items.Sum(it => it.SubTotal);

        public int NumberOfItems => Items.Sum(it => it.Quantity);

        public override bool Equals(object obj)
        {
            return obj is Order order && order.Number == Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number);
        }
    }
}
