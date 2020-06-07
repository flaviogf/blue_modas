namespace BlueModas.Web.ViewModels
{
    public class OrderItemShowViewModel
    {
        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }
    }
}
