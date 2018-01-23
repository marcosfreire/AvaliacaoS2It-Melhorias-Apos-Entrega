using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Desafio.s2.Domain.Core.Notifications;

namespace Desafio.s2.Site.Controllers
{
    [Authorize]
    public class AmigoController : BaseController
    {

        private readonly IAmigoAppService _amigoAppService;

        public AmigoController(
            IAmigoAppService service,
            IUser user,
            INotificationHandler<DomainNotification> notification,
            IMediatorHandler mediatr) : base(notification, user, mediatr)
        {
            _amigoAppService = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var amigos = _amigoAppService.ObterTodos();
            return View(amigos);
        }

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(AmigoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return View(model);
            }

            _amigoAppService.Adicionar(model);

            TempData["RetornoPost"] = OperacaoValida()
                ? "success,Amigo cadastrado com sucesso!"
                : "error,Amigo não cadastrado! Verifique as mensagens";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            var model = _amigoAppService.ObterPorId(id);
            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            var model = _amigoAppService.ObterPorId(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AmigoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return View(model);
            }

            _amigoAppService.Atualizar(model);

            TempData["RetornoPost"] = OperacaoValida()
                ? "success,Amigo atualizado com sucesso!"
                : "error,Amigo não atualizado! Verifique as mensagens";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            var model = _amigoAppService.ObterPorId(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            _amigoAppService.Remover(id);

            TempData["RetornoPost"] = OperacaoValida()
                ? "success,Amigo excluido com sucesso!"
                : "error,Amigo não excluido! Verifique as mensagens";

            return RedirectToAction(nameof(Index));
        }
    }
}