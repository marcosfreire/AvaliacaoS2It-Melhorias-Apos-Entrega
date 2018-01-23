using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Core.Notifications;
using Desafio.s2.Domain.Interfaces;
using MediatR;

namespace Desafios2.Api.Controllers
{
    public class AccountController : BaseController
    {
        protected AccountController(INotificationHandler<DomainNotification> notifications,
                                    IUser user,
                                    IMediatorHandler mediator) : base(notifications, user, mediator)
        {
        }
    }
}
