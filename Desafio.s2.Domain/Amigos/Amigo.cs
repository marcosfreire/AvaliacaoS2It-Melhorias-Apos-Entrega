using System;
using FluentValidation;
using Desafio.s2.Domain.Jogos;
using System.Collections.Generic;
using Desafio.s2.Domain.Core.Models;
using Desafio.s2.Domain.Constantes;

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
            ValidarIdUsuarioLogado();
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
                .NotEmpty().WithMessage(AmigoConstantes.NOME_OBRIGATORIO)
                .Length(2, 150).WithMessage(AmigoConstantes.MAX_MIN_LENTH_NOME);
        }

        private void ValidarIdUsuarioLogado()
        {
            RuleFor(c => c.IdUsuario).NotEqual(Guid.Empty).WithMessage(AmigoConstantes.ID_USUARIO_LOGADO_OBRITORIO);
        }

        private void ValidarEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(AmigoConstantes.EMAIL_OBRIGATORIO)
                .EmailAddress().WithMessage(AmigoConstantes.EMAIL_INVALIDO)
                .Length(2, 150).WithMessage(AmigoConstantes.MAX_MIN_LENTH_EMAIL);
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