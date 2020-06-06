using System;
using System.Collections.Generic;

namespace BlueModas.Api.Models
{
    public class Order
    {
        public Guid Number { get; set; }

        public Customer Customer { get; set; }

        public ISet<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

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
