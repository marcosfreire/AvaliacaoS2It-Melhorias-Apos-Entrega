using Desafio.s2.Domain.Core.Commands;
using Desafio.s2.Domain.Core.Models;
using System.Threading.Tasks;

namespace Desafio.s2.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}
