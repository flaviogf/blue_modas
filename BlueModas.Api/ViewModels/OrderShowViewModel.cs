using System;
using System.Collections.Generic;

namespace BlueModas.Api.ViewModels
{
    public class OrderShowViewModel
    {
        public Guid Number { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }

        public IList<OrderItemShowViewModel> Items { get; set; } = new List<OrderItemShowViewModel>();

        public decimal Total { get; set; }

        public int NumberOfItems { get; set; }
    }
}
