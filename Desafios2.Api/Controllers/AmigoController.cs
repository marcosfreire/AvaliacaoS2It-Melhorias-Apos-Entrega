using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Core.Notifications;

namespace Desafios2.Api.Controllers
{
    [Route("api/amigo")]
    public class AmigoController : BaseController
    {
        private readonly IAmigoAppService _amigoAppService;

        public AmigoController(INotificationHandler<DomainNotification> notifications,
                                  IUser user,
                                  IMediatorHandler mediator,
                                  IAmigoAppService service) : base(notifications, user, mediator)

        {
            _amigoAppService = service;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var amigo = _amigoAppService.ObterPorId(id);
            return Response(amigo);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]AmigoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return View(model);
            }

            _amigoAppService.Adicionar(model);

            return Response(model);
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody]AmigoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return View(model);
            }

            return Response(model);
        }

        [HttpDelete]
        [Route("")]
        public IActionResult Delete(Guid id)
        {
            _amigoAppService.Remover(id);
            return Response(id);
        }
    }
}
