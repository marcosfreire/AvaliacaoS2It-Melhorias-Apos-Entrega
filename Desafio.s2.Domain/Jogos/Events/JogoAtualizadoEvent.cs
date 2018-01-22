using System;
using Desafio.s2.Domain.Core.Models;

namespace Desafio.s2.Domain.Jogos.Events
{
    public class JogoAtualizadoEvent : Event
    {
        Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid CategoriaId { get; set; }
        public string ThumbnailCapaJogo { get; set; }

        public JogoAtualizadoEvent(Guid id, string nome, string thumnailCapaJogo, Guid categoriaId)
        {
            Id = id;
            Nome = nome;
            CategoriaId = categoriaId;
            ThumbnailCapaJogo = thumnailCapaJogo;
        }
    }
}
