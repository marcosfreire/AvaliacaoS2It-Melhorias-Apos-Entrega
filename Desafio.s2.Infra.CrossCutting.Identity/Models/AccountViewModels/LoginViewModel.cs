using System.ComponentModel.DataAnnotations;

namespace Desafio.s2.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Campo obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha?")]
        public string Password { get; set; }

        [Display(Name = "Continuar logado?")]
        public bool RememberMe { get; set; }
    }
}
