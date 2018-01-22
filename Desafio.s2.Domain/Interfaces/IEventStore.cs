using Desafio.s2.Domain.Core.Models;

namespace Desafio.s2.Domain.Core.Interfaces
{
    public interface IEventStore
    {
        void SalvarEvento<T>(T evento) where T : Event;
    }
}