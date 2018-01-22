using System;
using Desafio.s2.Domain.Core.Commands;

namespace Desafio.s2.Domain.Amigos.Commands
{
    public class AtualizarAmigoCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Guid IdUsuario { get; private set; }

        public AtualizarAmigoCommand(Guid id , string nome , string email , Guid idUsuario)
        {
            Id = id;
            Nome = nome;
            Email = email;
            IdUsuario = idUsuario;
        }
    }
}