using MediatR;
using Microsoft.AspNetCore.Mvc;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Core.Notifications;
using Desafio.s2.Domain.Interfaces;

namespace Desafio.s2.Site.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAmigoAppService _amigoAppService;

        public HomeController(IAmigoAppService amigoAppService, IUser user,
                               INotificationHandler<DomainNotification> notification,IMediatorHandler mediatr) : base(notification, user, mediatr)
        {
            _amigoAppService = amigoAppService;
        }

        public IActionResult Index()
        {
            //var model = new AmigoViewModel();
            //_amigoAppService.Adicionar(model);

            //ViewBag.RetornoPost = OperacaoValida()
            //    ? "success,Amigo cadastrado com sucesso!"
            //    : "error,Amigo não cadastrado! Verifique as mensagens";

            return View();
        }
    }
}