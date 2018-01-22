using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.s2.App.Service.ViewModels
{
    public class JogoViewModel
    {
        public JogoViewModel()
        {

        }

        [Key]
        public Guid Id { get;  set; }
        
        public Guid? EmprestadoParaId { get; set; }
        public AmigoViewModel EmprestadoPara { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get;  set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        public Guid CategoriaId { get;  set; }
        public CategoriaViewModel Categoria { get; set; }

        public string ThumbnailCapaJogo { get;  set; }

        public Guid IdUsuario { get; set; }
    }
}