using System;
using System.Linq;
using Desafio.s2.Data.Context;
using Desafio.s2.Domain.Amigos;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Amigos.Repository;

namespace Desafio.s2.Data.Repository
{
    public class AmigoRepository : Repository<Amigo>, IAmigoRepository
    {
        private readonly IUser _user;

        public AmigoRepository(ApplicationDataContext context, IUser user) : base(context)
        {
            _user = user;
        }

        public override IEnumerable<Amigo> ObterTodos()
        {
            return Db.Amigos
                .Where(a => !a.Excluido)
                .Include(a => a.Jogos)
                .Where(a => a.IdUsuario == _user.GetUserId())
                .ToList();
        }

        public override Amigo ObterPorId(Guid id)
        {
            var amigo = Db.Amigos.Include(a => a.Jogos)
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == id && !a.Excluido);

            return amigo;
        }
    }
}