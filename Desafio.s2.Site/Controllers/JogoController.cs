using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Desafio.s2.Domain.Extensions;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Desafio.s2.Domain.Core.Notifications;

namespace Desafio.s2.Site.Controllers
{
    [Authorize]
    public class JogoController : BaseController
    {
        private readonly IJogoAppService _jogoAppService;
        private readonly IAmigoAppService _amigoAppService;

        public JogoController(IJogoAppService service,
                              IUser user,
                              IAmigoAppService amigoAppService,
                              INotificationHandler<DomainNotification> notification,
                              IMediatorHandler mediatr) : base(notification, user, mediatr)
        {
            _jogoAppService = service;
            _amigoAppService = amigoAppService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var jogos = _jogoAppService.ObterTodos();
            return View(jogos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CarregarComboCategoria();
            return View();
        }

        [HttpPost]
        public IActionResult Create(JogoViewModel model, IFormFile ThumbnailCapaJogo)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                CarregarComboCategoria();
                return View(model);
            }

            if (ThumbnailCapaJogo != null)
                model.ThumbnailCapaJogo = ThumbnailCapaJogo.Base64Image();

            _jogoAppService.Adicionar(model);

            TempData["RetornoPost"] = OperacaoValida() 
                ? "success,Jogo cadastrado com sucesso!" 
                : "error,Jogo não cadastrado! Verifique as mensagens";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            var model = _jogoAppService.ObterPorId(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            var model = _jogoAppService.ObterPorId(id);

            CarregarComboCategoria();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(JogoViewModel model, IFormFile ThumbnailCapaJogo)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                CarregarComboCategoria();
                return View(model);
            }

            model.ThumbnailCapaJogo = ThumbnailCapaJogo.Base64Image();

            _jogoAppService.Atualizar(model);

            TempData["RetornoPost"] = OperacaoValida()
                ? "success,Jogo atualizado com sucesso!"
                : "error,Jogo não atualizado! Verifique as mensagens";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            var model = _jogoAppService.ObterPorId(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            _jogoAppService.Remover(id);

            TempData["RetornoPost"] = OperacaoValida() 
                ? "success,Jogo excluido com sucesso!" 
                : "error,Jogo não excluido! Verifique as mensagens";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Emprestar(Guid id)
        {
            if (id.Equals(Guid.Empty)) return BadRequest();

            var model = _jogoAppService.ObterPorId(id);

            CarregarComboAmigos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Emprestar(JogoViewModel model)
        {
            var jogo = _jogoAppService.ObterPorId(model.Id);
            jogo.Categoria = null;
            jogo.EmprestadoPara = null;
            jogo.EmprestadoParaId = model.EmprestadoParaId;

            _jogoAppService.Atualizar(jogo);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Devolver(JogoViewModel model)
        {
            var jogo = _jogoAppService.ObterPorId(model.Id);

            jogo.Categoria = null;
            jogo.EmprestadoPara = null;
            jogo.EmprestadoParaId = null;

            _jogoAppService.Atualizar(jogo);

            return RedirectToAction(nameof(Index));
        }

        private void CarregarComboCategoria()
        {
            ViewBag.Categorias = Data.Context.DbInitializer.GetCategorias();
        }

        private void CarregarComboAmigos()
        {
            var amigos = _amigoAppService.ObterTodos();
            ViewBag.Amigos = amigos;
        }
    }
}