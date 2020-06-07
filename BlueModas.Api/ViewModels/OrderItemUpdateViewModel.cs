using System.ComponentModel.DataAnnotations;

namespace BlueModas.Api.ViewModels
{
    public class OrderItemUpdateViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
