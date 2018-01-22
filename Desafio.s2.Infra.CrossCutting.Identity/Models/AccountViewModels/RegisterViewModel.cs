using System.ComponentModel.DataAnnotations;

namespace Desafio.s2.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Campo obrigatório")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Campo obrigatório")]
        [StringLength(100, ErrorMessage = "O campo senha deve possuir ao menos 6 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "A senha e confirmação de senha não coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
