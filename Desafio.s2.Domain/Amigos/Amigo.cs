using System;
using FluentValidation;
using Desafio.s2.Domain.Jogos;
using System.Collections.Generic;
using Desafio.s2.Domain.Core.Models;

namespace Desafio.s2.Domain.Amigos
{
    public class Amigo : Entity<Amigo>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Guid IdUsuario { get; private set; }

        public virtual ICollection<Jogo> Jogos { get; set; }

        private Amigo() { }

        public Amigo(string nome , string email , Guid idUsuario)
        {
            Nome = nome;
            Email = email;
            IdUsuario = idUsuario;
        }

        #region methods

        public override bool EhValido()
        {
            ValidarNome();
            ValidarEmail();
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public override void Excluir()
        {
            Excluido = true;
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo Nome deve ser informado")
                .Length(2, 150).WithMessage("O campo Nome precisa ter entre 2 e 150 caracteres");
        }

        private void ValidarEmail()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo Email deve ser informado")
                //.EmailAddress().WithMessage("Email inválido")
                .Length(2, 150).WithMessage("O campo Email precisa ter entre 2 e 150 caracteres");            
        }

        public static class AmigoFactory
        {
            public static Amigo NovoAmigoCompleto(Guid id, string nome, string email,Guid idUsuario)
            {
                var amigo = new Amigo()
                {
                    Id = id,
                    Nome = nome,
                    Email = email,
                    IdUsuario = idUsuario
                };

                return amigo;
            }
        }

        #endregion
    }
}