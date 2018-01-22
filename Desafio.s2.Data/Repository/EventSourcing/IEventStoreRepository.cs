using System;
using System.Collections.Generic;
using Desafio.s2.Domain.Core.Models;

namespace Desafio.s2.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository 
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}