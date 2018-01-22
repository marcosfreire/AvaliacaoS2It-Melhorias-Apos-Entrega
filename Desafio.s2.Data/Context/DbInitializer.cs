using System;
using System.Collections.Generic;
using System.Linq;
using Desafio.s2.Domain.Jogos;

namespace Desafio.s2.Data.Context
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categorias.Any()) return;

            var categorias = GetCategorias();

            foreach (var categoria in categorias)
                context.Categorias.Add(categoria);

            context.SaveChanges();
        }

        public static List<Categoria> GetCategorias() => new List<Categoria>
        {
            new Categoria(Guid.Parse("991106bf-9544-48f9-8898-22e208fccc42"),"Rpg"),
            new Categoria(Guid.Parse("042a0042-16e4-4aa7-b00e-1c2b17e78b50"),"Fps"),
            new Categoria(Guid.Parse("f4d09bdc-0952-4edf-be41-d579d844ea47"),"Corrida"),
            new Categoria(Guid.Parse("7bf21f04-5be3-438d-85d1-047e54e9a45d"),"Multiplayer")
        };
    }
}