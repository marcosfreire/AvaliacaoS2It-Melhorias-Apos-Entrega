using System;
using System.Linq.Expressions;
using Desafio.s2.Domain.Amigos;
using System.Collections.Generic;
using Desafio.s2.App.Service.ViewModels;

namespace Desafio.s2.App.Service.Interfaces
{
    public interface IAmigoAppService
    {
        void Remover(Guid id);
        void Adicionar(AmigoViewModel amigoViewModel);
        void Atualizar(AmigoViewModel amigoViewModel);
        AmigoViewModel ObterPorId(Guid id);
        IEnumerable<AmigoViewModel> ObterTodos();
        IEnumerable<AmigoViewModel> Buscar(Expression<Func<Amigo, bool>> predicate);
    }
}