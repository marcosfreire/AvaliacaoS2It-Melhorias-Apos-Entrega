using System;
using Desafio.s2.Domain.Core.Commands;

namespace Desafio.s2.Domain.Jogos.Commands
{
    public class RegistrarJogoCommand : Command
    {
        public string Nome { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string ThumbnailCapaJogo { get; private set; }
        public Guid IdUsuario { get; set; }

        public RegistrarJogoCommand(string nome, string imagemCapa, Guid categoriaId, Guid idUsuario)
        {
            Nome = nome;
            IdUsuario = IdUsuario;
            CategoriaId = categoriaId;
            ThumbnailCapaJogo = imagemCapa;
            IdUsuario = idUsuario;
        }
    }
}