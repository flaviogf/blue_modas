namespace BlueModas.Api.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
