using System;
using System.Collections.Generic;

namespace Desafio.s2.App.Service.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<JogoViewModel> Jogos { get; set; }
    }
}