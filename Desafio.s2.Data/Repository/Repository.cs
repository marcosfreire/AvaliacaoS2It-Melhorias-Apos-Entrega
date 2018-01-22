using System;
using System.Linq;
using System.Linq.Expressions;
using Desafio.s2.Data.Context;
using System.Collections.Generic;
using Desafio.s2.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using Desafio.s2.Domain.Core.Interfaces;

namespace Desafio.s2.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected DbSet<TEntity> DbSet;
        protected ApplicationDataContext Db;
        
        protected Repository(ApplicationDataContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking()
                .Where(a => !a.Excluido)
                .Where(predicate);
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id && !t.Excluido) ;
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.Where(a => !a.Excluido).ToList();
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }        
    }
}