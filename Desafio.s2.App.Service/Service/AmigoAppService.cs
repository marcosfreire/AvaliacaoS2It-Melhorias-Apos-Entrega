using System;
using AutoMapper;
using System.Linq;
using System.Linq.Expressions;
using Desafio.s2.Domain.Amigos;
using System.Collections.Generic;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Amigos.Commands;
using Desafio.s2.Domain.Amigos.Repository;
using Desafio.s2.Domain.Core.Interfaces;

namespace Desafio.s2.App.Service.Service
{
    public class AmigoAppService : IAmigoAppService
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IAmigoRepository _repository;

        public AmigoAppService(IMediatorHandler bus, IAmigoRepository repository, IMapper mapper , IUser user)
        {
            _bus = bus;
            _user = user;
            _mapper = mapper;
            _repository = repository;
        }

        public void Adicionar(AmigoViewModel viewModel)
        {
            var command = new RegistrarAmigoCommand(viewModel.Nome, viewModel.Email, _user.GetUserId());
            _bus.EnviarComando(command);
        }

        public void Atualizar(AmigoViewModel viewModel)
        {
            var command = new AtualizarAmigoCommand(viewModel.Id, viewModel.Nome, viewModel.Email, _user.GetUserId());
            _bus.EnviarComando(command);
        }

        public void Remover(Guid id)
        {
            var command = new ExcluirAmigoCommand(id);
            _bus.EnviarComando(command);
        }

        public IEnumerable<AmigoViewModel> Buscar(Expression<Func<Amigo, bool>> predicate)
        {
            var amigos = _repository.Buscar(predicate);
            return _mapper.Map<IEnumerable<Amigo>, IEnumerable<AmigoViewModel>>(amigos);
        }

        public AmigoViewModel ObterPorId(Guid id)
        {
            // todo : refactor here

            var amigo = _repository.ObterPorId(id);

            if (amigo != null)
                amigo.Jogos?.ToList()?.ForEach(a => a.EmprestadoPara = null);

            return _mapper.Map<Amigo, AmigoViewModel>(amigo);
        }

        public IEnumerable<AmigoViewModel> ObterTodos()
        {
            var amigos = _repository.ObterTodos();
            return _mapper.Map<IEnumerable<Amigo>, IEnumerable<AmigoViewModel>>(amigos);
        }
    }
}
