using AutoMapper;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Amigos;
using Desafio.s2.Domain.Jogos;

namespace Desafio.s2.App.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Amigo, AmigoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Jogo, JogoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
