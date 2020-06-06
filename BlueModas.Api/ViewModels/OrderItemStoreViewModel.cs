using System.ComponentModel.DataAnnotations;

namespace BlueModas.Api.ViewModels
{
    public class OrderItemStoreViewModel
    {
        [Required]
        public int ProductId { get; set; }
    }
}
