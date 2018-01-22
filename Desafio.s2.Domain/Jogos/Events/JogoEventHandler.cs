using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.s2.Domain.Jogos.Events
{
    public class JogoEventHandler
        : INotificationHandler<JogoRegistradoEvent>,
          INotificationHandler<JogoAtualizadoEvent>,
          INotificationHandler<JogoExcluidoEvent>
    {
        public Task Handle(JogoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            //todo :  verificar regra de negocio
            return Task.CompletedTask;
        }

        public Task Handle(JogoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            //todo :  verificar regra de negocio
            return Task.CompletedTask;
        }

        public Task Handle(JogoExcluidoEvent notification, CancellationToken cancellationToken)
        {
            //todo :  verificar regra de negocio
            return Task.CompletedTask;
        }
    }
}