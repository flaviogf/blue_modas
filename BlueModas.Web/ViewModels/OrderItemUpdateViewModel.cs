using System.ComponentModel.DataAnnotations;

namespace BlueModas.Web.ViewModels
{
    public class OrderItemUpdateViewModel
    {
        [Required(ErrorMessage = "Campo {0} é requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Campo {0} deve conter um valor entre {1} e {2}")]
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }
    }
}
