using System;
using System.Linq;
using Desafio.s2.Data.Context;
using Desafio.s2.Domain.Jogos;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Jogos.Repository;

namespace Desafio.s2.Data.Repository
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        private readonly IUser _user;
        public JogoRepository(ApplicationDataContext context , IUser user) : base(context)
        {
            _user = user;
        }

        public override IEnumerable<Jogo> ObterTodos()
        {
            return Db.Jogos
                .Where(a => !a.Excluido)
                .Where(a => a.IdUsuario == _user.GetUserId())
                .Include(a => a.Categoria)
                .ToList();
        }

        public override Jogo ObterPorId(Guid id)
        {
            return DbSet.AsNoTracking()
                .Include(a => a.Categoria)
                .Include(a => a.EmprestadoPara)
                .FirstOrDefault(t => t.Id == id && !t.Excluido);
        }
    }
}