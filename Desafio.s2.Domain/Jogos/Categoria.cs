using System;
using System.Collections.Generic;
using Desafio.s2.Domain.Core.Models;

namespace Desafio.s2.Domain.Jogos
{
    public class Categoria : Entity<Categoria>
    {
        protected Categoria() { }

        public string Nome { get; private set; }

        public virtual ICollection<Jogo> Jogos { get; set; }

        public Categoria(Guid id , string nome)
        {
            Id = id;
            Nome = nome;
        }
        
        public override bool EhValido()
        {
            return true;
        }

        public override void Excluir()
        {
            Excluido = false;
        }
    }
}