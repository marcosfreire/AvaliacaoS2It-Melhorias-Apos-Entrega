using Desafio.s2.Domain.Core.Commands;
using System;

namespace Desafio.s2.Domain.Jogos.Commands
{
    public class AtualizarJogoCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid? EmprestadoParaId { get; private set; }
        public string ThumbnailCapaJogo { get; private set; }

        public Guid IdUsuario { get; set; }

        public AtualizarJogoCommand(Guid id , string nome, string imagemCapa, Guid categoriaId , Guid? emprestadoParaId ,Guid idUsuario)
        {
            Id = id;
            Nome = nome;
            IdUsuario = idUsuario;
            CategoriaId = categoriaId;
            ThumbnailCapaJogo = imagemCapa;
            EmprestadoParaId = emprestadoParaId;
        }
    }
}