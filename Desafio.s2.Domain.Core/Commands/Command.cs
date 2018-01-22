using Desafio.s2.Domain.Core.Models;
using System;

namespace Desafio.s2.Domain.Core.Commands
{
    public class Command : Message
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
