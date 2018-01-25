using System;
using MediatR;
using Desafio.s2.Site.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Desafio.s2.Domain.Extensions;
using Desafio.s2.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Desafio.s2.Domain.Core.Notifications;
using Desafio.s2.Infra.CrossCutting.Identity.Services;

namespace Desafio.s2.Site.Controllers
{
    [Authorize]
    public class JogoController : BaseController
    {
        private readonly IUser _user;
        private readonly IEmailSender _emailSender;
        private readonly EmailSettings _emailSettings;
        private readonly IJogoAppService _jogoAppService;
        private readonly IAmigoAppService _amigoAppService;

        public JogoController(IUser user,
                              IJogoAppService service,
                              IMediatorHandler mediatr,
                              IEmailSender emailSender,
                              IAmigoAppService amigoAppService,
                              IOptions<EmailSettings> emailSettings,
                              INotificationHandler<DomainNotification> notification) : base(notification, user, mediatr)
        {
            _user = user;
            _jogoAppService = service;
            _emailSender = emailSender;
            _amigoAppService = amigoAppService;
            _emailSettings = emailSettings.Value;
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

            TempData["RetornoPost"] = "warning,Jogo emprestado com sucesso!";

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

            TempData["RetornoPost"] = "success,Jogo devoldido com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult SolicitarDevolucao(JogoViewModel model)
        {
            try
            {
                _emailSettings.FromEmail = _user.Name;
                _emailSender.EnviarEmailCobrancaJogoAsync(_emailSettings, model);

                TempData["Devolucao"] = "success,Devolução de jogo solicitada com sucesso!";
            }
            catch
            {
                TempData["Devolucao"] = "error,Ops, ocorreu um erro ao solicitar a devolução do jogo!";
            }

            return View("Emprestar", model);
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