using Desafio.s2.Domain.Core.Models;
using System;

namespace Desafio.s2.Domain.Amigos.Events
{
    public class AmigoAtualizadoEvent : Event
    {
        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public AmigoAtualizadoEvent(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}
