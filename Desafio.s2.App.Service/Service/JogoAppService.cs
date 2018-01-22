using System;
using AutoMapper;
using System.Linq;
using Desafio.s2.Domain.Jogos;
using System.Linq.Expressions;
using System.Collections.Generic;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.Domain.Jogos.Commands;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Jogos.Repository;
using Desafio.s2.Domain.Core.Interfaces;

namespace Desafio.s2.App.Service.Service
{
    public class JogoAppService : IJogoAppService
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IJogoRepository _repository;

        public JogoAppService(IMapper mapper, IMediatorHandler mediatr, IJogoRepository repository,IUser user)
        {
            _user = user;
            _bus = mediatr;            
            _mapper = mapper;
            _repository = repository;
        }

        public void Adicionar(JogoViewModel viewModel)
        {
            var command = new RegistrarJogoCommand(viewModel.Nome, viewModel.ThumbnailCapaJogo, viewModel.CategoriaId, _user.GetUserId());
            _bus.EnviarComando(command);
        }

        public void Atualizar(JogoViewModel viewModel)
        {
            var command = new AtualizarJogoCommand(viewModel.Id, viewModel.Nome, viewModel.ThumbnailCapaJogo, viewModel.CategoriaId, viewModel.EmprestadoParaId, _user.GetUserId());
            _bus.EnviarComando(command);
        }

        public void Remover(Guid id)
        {
            var command = new ExcluidJogoCommand(id);
            _bus.EnviarComando(command);
        }

        public IEnumerable<JogoViewModel> Buscar(Expression<Func<Jogo, bool>> predicate)
        {
            var jogos = _repository.Buscar(predicate);
            return _mapper.Map<IEnumerable<Jogo>, IEnumerable<JogoViewModel>>(jogos);
        }

        public JogoViewModel ObterPorId(Guid id)
        {
            // TODO :  Configurar Dapper e LazyLoad
            var jogo = _repository.ObterPorId(id);

            if (jogo != null)
            {
                jogo.Categoria.Jogos = null;

                if (jogo.EmprestadoPara != null)
                    jogo.EmprestadoPara.Jogos = null;
            }

            return _mapper.Map<Jogo, JogoViewModel>(jogo);
        }

        public IEnumerable<JogoViewModel> ObterTodos()
        {
            // TODO :  Configurar Dapper e LazyLoad
            var jogos = _repository.ObterTodos().ToList();
            jogos.ForEach(a => a.Categoria.Jogos = null);
            return _mapper.Map<IEnumerable<Jogo>, IEnumerable<JogoViewModel>>(jogos);
        }
    }
}