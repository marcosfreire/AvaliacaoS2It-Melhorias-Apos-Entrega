using Desafio.s2.Domain.Core.Commands;
using System;

namespace Desafio.s2.Domain.Amigos.Commands
{
    public class RegistrarAmigoCommand : Command
    {
        public Guid IdUsuario { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public RegistrarAmigoCommand(string nome , string email, Guid idUsuario)
        {
            Nome = nome;
            Email = email;
            IdUsuario = idUsuario;            
        }
    }
}