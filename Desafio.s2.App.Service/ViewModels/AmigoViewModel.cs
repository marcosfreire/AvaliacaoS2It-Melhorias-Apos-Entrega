using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Desafio.s2.App.Service.ViewModels
{
    public class AmigoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }

        public Guid IdUsuario { get; set; }

        public List<JogoViewModel> Jogos { get; set; }
    }
}