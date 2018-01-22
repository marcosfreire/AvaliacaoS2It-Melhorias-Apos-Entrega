using System;
using Desafio.s2.Domain.Core.Commands;

namespace Desafio.s2.Domain.Amigos.Commands
{
    public class ExcluirAmigoCommand : Command
    {
        public Guid Id { get; private set; }

        public ExcluirAmigoCommand(Guid id)
        {
            Id = id;
        }
    }
}