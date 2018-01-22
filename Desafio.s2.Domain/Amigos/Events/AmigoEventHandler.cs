using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.s2.Domain.Amigos.Events
{
    public class AmigoEventHandler :
        INotificationHandler<AmigoRegistradoEvent>,
        INotificationHandler<AmigoAtualizadoEvent>,
        INotificationHandler<AmigoExcluidoEvent>
    {
        public Task Handle(AmigoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            //todo :  verificar regra de negocio
            return Task.CompletedTask;
        }

        public Task Handle(AmigoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            //todo :  verificar regra de negocio
            return Task.CompletedTask;
        }

        public Task Handle(AmigoExcluidoEvent notification, CancellationToken cancellationToken)
        {
            //todo :  verificar regra de negocio
            return Task.CompletedTask;
        }
    }
}
