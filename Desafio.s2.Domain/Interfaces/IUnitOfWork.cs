using System;

namespace Desafio.s2.Domain.Core.Interfaces
{
    public interface IUnitOfWork 
    {
        bool Commit();
    }
}