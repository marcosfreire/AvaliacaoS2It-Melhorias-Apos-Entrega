using Desafio.s2.Domain.Core.Models;
using System;

namespace Desafio.s2.Domain.Amigos.Events
{
    public class AmigoExcluidoEvent : Event
    {
        public Guid Id { get; set; }
       
        public AmigoExcluidoEvent(Guid id )
        {
            Id = id;          
        }
    }
}
