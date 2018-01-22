using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Jogos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Desafio.s2.App.Service.Interfaces
{
    public interface IJogoAppService
    {
        void Remover(Guid id);
        void Adicionar(JogoViewModel jogoViewModel);
        void Atualizar(JogoViewModel jogoViewModel);
        JogoViewModel ObterPorId(Guid id);
        IEnumerable<JogoViewModel> ObterTodos();
        IEnumerable<JogoViewModel> Buscar(Expression<Func<Jogo, bool>> predicate);
    }
}