using System;

namespace Desafio.s2.Domain.Core.Models
{
    public class StoredEvent : Event
    {
        protected StoredEvent() { }

        public StoredEvent(Event evento, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = evento.AggregateId;
            MessageType = evento.MessageType;
            Data = data;
            User = user;
        }        

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}