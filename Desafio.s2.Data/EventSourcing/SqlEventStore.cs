using Newtonsoft.Json;
using Desafio.s2.Domain.Core.Models;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Data.Repository.EventSourcing;

namespace Desafio.s2.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _user = user;
            _eventStoreRepository = eventStoreRepository;            
        }

        public void SalvarEvento<T>(T evento) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(evento);

            var storedEvent = new StoredEvent(
                evento,
                serializedData,
                _user.GetUserId().ToString());

            _eventStoreRepository.Store(storedEvent);
        }
    }
}