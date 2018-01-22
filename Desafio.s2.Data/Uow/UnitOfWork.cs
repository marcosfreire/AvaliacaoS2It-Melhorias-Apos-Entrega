using Desafio.s2.Data.Context;
using Desafio.s2.Domain.Core.Interfaces;

namespace Desafio.s2.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDataContext _context;

        public UnitOfWork(ApplicationDataContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
        
    }
}