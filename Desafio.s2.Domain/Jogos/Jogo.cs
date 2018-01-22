using System;
using FluentValidation;
using Desafio.s2.Domain.Amigos;
using Desafio.s2.Domain.Core.Models;

namespace Desafio.s2.Domain.Jogos
{
    public class Jogo : Entity<Jogo>
    {
        private Jogo() { }
        public Jogo(string nome, Guid categoriaId , Guid idUsuario)
        {
            Nome = nome;
            Id = Guid.NewGuid();
            IdUsuario = idUsuario;
            CategoriaId = categoriaId;            
        }

        public string Nome { get; private set; }
        public string ThumbnailCapaJogo { get; private set; }

        public Guid? CategoriaId { get; private set; }
        public virtual Categoria Categoria { get; private set; }

        public Guid? EmprestadoParaId { get; set; }
        public virtual Amigo EmprestadoPara { get; set; }

        public Guid IdUsuario { get; private set; }

        #region methods

        public void AtribuirCapaJogo(string thumbnailCapaJogo)
        {
            ThumbnailCapaJogo = thumbnailCapaJogo;
        }

        public override bool EhValido()
        {
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

        private void ValidarCategoria()
        {
            RuleFor(c => c.CategoriaId)
                .NotNull().WithMessage("O campo Categoria do jogo deve ser informado");
        }

        public static class JogoFactory
        {
            public static Jogo NovoJogoCompleto(Guid id, string nome, string thumbnailCapaJogo, Guid categoriaId , Guid? emprestadoParaId , Guid idUsuario)
            {
                var evento = new Jogo()
                {
                    Id = id,
                    Nome = nome,
                    IdUsuario = idUsuario,
                    CategoriaId = categoriaId,
                    EmprestadoParaId = emprestadoParaId,
                    ThumbnailCapaJogo = thumbnailCapaJogo
                };

                return evento;
            }
        }

        #endregion
    }
}