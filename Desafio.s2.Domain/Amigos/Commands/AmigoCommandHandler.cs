using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.Domain.Amigos.Events;
using Desafio.s2.Domain.Core.Handlers;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Amigos.Repository;
using Desafio.s2.Domain.Core.Notifications;
using static Desafio.s2.Domain.Amigos.Amigo;

namespace Desafio.s2.Domain.Amigos.Commands
{
    public class AmigoCommandHandler : CommandHandler
        , INotificationHandler<RegistrarAmigoCommand>
        , INotificationHandler<AtualizarAmigoCommand>
        , INotificationHandler<ExcluirAmigoCommand>
    {
        private readonly IUser _user;
        private readonly IMediatorHandler _mediator;
        private readonly IAmigoRepository _amigoRepository;

        public AmigoCommandHandler(IUnitOfWork uow,
                                   INotificationHandler<DomainNotification> notifications,
                                   IUser user,
                                   IMediatorHandler mediator,
                                   IAmigoRepository amigoRepository) : base(uow, mediator, notifications)

        {
            _user = user;
            _mediator = mediator;
            _amigoRepository = amigoRepository;
        }

        private bool AmigoEhValido(Amigo amigo)
        {
            if (amigo.EhValido()) return true;

            NotificarValidacoesErro(amigo.ValidationResult);
            return false;
        }

        public Task Handle(RegistrarAmigoCommand command, CancellationToken cancellationToken)
        {
            var amigo = new Amigo(command.Nome, command.Email, command.IdUsuario);

            if (!AmigoEhValido(amigo)) return Task.CompletedTask;

            _amigoRepository.Adicionar(amigo);

            if (Commit())
                _mediator.PublicarEvento(new AmigoRegistradoEvent(amigo.Id, amigo.Nome, amigo.Email));

            return Task.CompletedTask;
        }

        public Task Handle(AtualizarAmigoCommand command, CancellationToken cancellationToken)
        {

            var amigo = AmigoFactory.NovoAmigoCompleto(command.Id, command.Nome, command.Email, command.IdUsuario);

            if (!AmigoEhValido(amigo)) return Task.CompletedTask;

            _amigoRepository.Atualizar(amigo);

            if (Commit())
                _mediator.PublicarEvento(new AmigoAtualizadoEvent(amigo.Id, amigo.Nome, amigo.Email));
            return Task.CompletedTask;
        }

        public Task Handle(ExcluirAmigoCommand notification, CancellationToken cancellationToken)
        {
            if (!AmigoExistente(notification.Id, notification.MessageType)) return Task.CompletedTask;

            var amigoAtual = _amigoRepository.ObterPorId(notification.Id);

            amigoAtual.Excluir();

            _amigoRepository.Atualizar(amigoAtual);

            if (Commit())
            {
                _mediator.PublicarEvento(new AmigoExcluidoEvent(notification.Id));
            }
            return Task.CompletedTask;
        }

        private bool AmigoExistente(Guid id, string messageType)
        {
            var amigo = _amigoRepository.ObterPorId(id);

            if (amigo != null) return true;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Amigo não encontrado."));
            return false;
        }
    }
}