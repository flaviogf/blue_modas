using System.ComponentModel.DataAnnotations;

namespace BlueModas.Api.ViewModels
{
    public class ProductStoreViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(255)]
        [Url]
        public string Image { get; set; }
    }
}
