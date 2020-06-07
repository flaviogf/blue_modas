using System.ComponentModel.DataAnnotations;

namespace BlueModas.Web.ViewModels
{
    public class OrderCustomerStoreViewModel
    {
        [Required(ErrorMessage = "Campo {0} é requerido")]
        [MaxLength(255, ErrorMessage = "Campo {0} deve conter no maxímo {1} caracteres")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo {0} é requerido")]
        [MaxLength(255, ErrorMessage = "Campo {0} deve conter no maxímo {1} caracteres")]
        [EmailAddress(ErrorMessage = "Campo {0} não é um e-mail válido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} é requerido")]
        [MaxLength(255, ErrorMessage = "Campo {0} deve conter no maxímo {1} caracteres")]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }
    }
}
