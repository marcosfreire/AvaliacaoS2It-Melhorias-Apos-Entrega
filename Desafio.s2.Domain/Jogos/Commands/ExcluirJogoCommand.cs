using System;
using Desafio.s2.Domain.Core.Commands;

namespace Desafio.s2.Domain.Jogos.Commands
{
    public class ExcluidJogoCommand : Command
    {
        public Guid Id { get; private set; }

        public ExcluidJogoCommand(Guid id)
        {
            Id = id;
        }
    }
}