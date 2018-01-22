using MediatR;
using Microsoft.AspNetCore.Mvc;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Core.Notifications;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Desafio.s2.Domain.Interfaces;

namespace Desafio.s2.Site.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUser _user;
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        public BaseController(INotificationHandler<DomainNotification> notifications, IUser user, IMediatorHandler mediator)
        {
            _user = user;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
        }

        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotificarErro(result.ToString(), error.Description);
            }
        }
    }
}