using System.ComponentModel.DataAnnotations;

namespace BlueModas.Api.ViewModels
{
    public class OrderCustomerStoreViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Phone { get; set; }
    }
}
