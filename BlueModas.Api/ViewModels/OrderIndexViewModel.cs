using System;

namespace BlueModas.Api.ViewModels
{
    public class OrderIndexViewModel
    {
        public Guid Number { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPhone { get; set; }
    }
}
