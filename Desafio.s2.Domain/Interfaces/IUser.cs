using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace Desafio.s2.Domain.Core.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}