using System;
using Desafio.s2.Domain.Core.Models;

namespace Desafio.s2.Domain.Jogos.Events
{
    public class JogoExcluidoEvent : Event
    {
        public Guid Id { get; set; }

        public JogoExcluidoEvent(Guid id)
        {
            Id = id;
        }
    }
}