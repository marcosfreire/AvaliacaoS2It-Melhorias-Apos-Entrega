using Desafio.s2.Domain.Core.Models;
using System;

namespace Desafio.s2.Domain.Amigos.Events
{
    public class AmigoRegistradoEvent : Event
    {
        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public AmigoRegistradoEvent(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}