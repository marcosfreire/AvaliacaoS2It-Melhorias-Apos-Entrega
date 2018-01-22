using Desafio.s2.Domain.Core.Models;
using System;

namespace Desafio.s2.Domain.Jogos.Events
{
    public class JogoRegistradoEvent : Event
    {
        Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid CategoriaId { get; set; }
        public string ThumbnailCapaJogo { get; set; }

        public JogoRegistradoEvent(Guid id , string nome, string thumnailCapaJogo, Guid categoriaId)
        {
            Id = id;
            Nome = nome;
            CategoriaId = categoriaId;
            ThumbnailCapaJogo = thumnailCapaJogo;
        }
    }
}
