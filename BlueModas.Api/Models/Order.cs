using System;
using System.Collections.Generic;

namespace BlueModas.Api.Models
{
    public class Order
    {
        public Guid Number { get; set; }

        public Customer Customer { get; set; }

        public IList<OrderItem> Items { get; set; }
    }
}
