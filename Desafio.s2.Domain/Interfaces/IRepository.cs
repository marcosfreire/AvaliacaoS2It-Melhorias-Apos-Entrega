using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Desafio.s2.Domain.Core.Models;

namespace Desafio.s2.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        void Remover(Guid id);
        void Adicionar(TEntity obj);
        void Atualizar(TEntity obj);
        TEntity ObterPorId(Guid id);
        IEnumerable<TEntity> ObterTodos();
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}