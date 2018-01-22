using System.ComponentModel.DataAnnotations;

namespace Desafio.s2.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Campo obrigatório")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
