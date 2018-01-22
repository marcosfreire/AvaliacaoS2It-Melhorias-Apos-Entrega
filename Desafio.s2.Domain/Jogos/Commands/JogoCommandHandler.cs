using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.Domain.Jogos.Events;
using Desafio.s2.Domain.Core.Handlers;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Jogos.Repository;
using static Desafio.s2.Domain.Jogos.Jogo;
using Desafio.s2.Domain.Core.Notifications;

namespace Desafio.s2.Domain.Jogos.Commands
{
    public class JogoCommandHandler : CommandHandler
        , INotificationHandler<RegistrarJogoCommand>
        , INotificationHandler<AtualizarJogoCommand>
        , INotificationHandler<ExcluidJogoCommand>
    {
        private readonly IUser _user;
        private readonly IMediatorHandler _mediator;
        private readonly IJogoRepository _jogoRepository;

        public JogoCommandHandler(IUnitOfWork uow,
                                   INotificationHandler<DomainNotification> notifications,
                                   IUser user,
                                   IMediatorHandler mediator,
                                   IJogoRepository jogoRepository) : base(uow, mediator, notifications)
        {
            _user = user;
            _mediator = mediator;
            _jogoRepository = jogoRepository;
        }

        private bool JogoEhValido(Jogo jogo)
        {
            if (jogo.EhValido()) return true;

            NotificarValidacoesErro(jogo.ValidationResult);
            return false;
        }

        public Task Handle(RegistrarJogoCommand command, CancellationToken cancellationToken)
        {
            var jogo = new Jogo(command.Nome, command.CategoriaId, command.IdUsuario);

            if (PossuiImagemCapa(command.ThumbnailCapaJogo))
                jogo.AtribuirCapaJogo(command.ThumbnailCapaJogo);

            if (!JogoEhValido(jogo)) return Task.CompletedTask;

            _jogoRepository.Adicionar(jogo);

            if (Commit())
                _mediator.PublicarEvento(new JogoRegistradoEvent(jogo.Id, jogo.Nome, jogo.ThumbnailCapaJogo, jogo.CategoriaId.Value));

            return Task.CompletedTask;
        }

        public Task Handle(AtualizarJogoCommand command, CancellationToken cancellationToken)
        {

            var jogo = JogoFactory
                .NovoJogoCompleto(command.Id, command.Nome, command.ThumbnailCapaJogo, command.CategoriaId, command.EmprestadoParaId, command.IdUsuario);

            if (PossuiImagemCapa(command.ThumbnailCapaJogo))
                jogo.AtribuirCapaJogo(command.ThumbnailCapaJogo);

            if (!JogoEhValido(jogo)) return Task.CompletedTask;

            _jogoRepository.Atualizar(jogo);

            if (Commit())
                _mediator.PublicarEvento(new JogoRegistradoEvent(jogo.Id, jogo.Nome, jogo.ThumbnailCapaJogo, jogo.CategoriaId.Value));

            return Task.CompletedTask;
        }

        public Task Handle(ExcluidJogoCommand notification, CancellationToken cancellationToken)
        {
            if (!JogoExistente(notification.Id, notification.MessageType)) return Task.CompletedTask;

            var jogoAtual = _jogoRepository.ObterPorId(notification.Id);

            jogoAtual.Excluir();

            _jogoRepository.Atualizar(jogoAtual);

            if (Commit())
            {
                _mediator.PublicarEvento(new JogoExcluidoEvent(notification.Id));
            }
            return Task.CompletedTask;
        }

        private bool JogoExistente(Guid id, string messageType)
        {
            var jogo = _jogoRepository.ObterPorId(id);

            if (jogo != null) return true;

            _mediator.PublicarEvento(new DomainNotification(messageType, "Jogo não encontrado."));
            return false;
        }

        private static bool PossuiImagemCapa(string thumbnailCapaJogo)
        {
            return !string.IsNullOrEmpty(thumbnailCapaJogo);
        }
    }
}