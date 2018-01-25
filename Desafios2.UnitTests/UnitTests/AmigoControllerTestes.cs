using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Desafio.s2.Site.Controllers;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.App.Service.Service;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.App.Service.ViewModels;
using Desafio.s2.Domain.Amigos.Commands;
using Desafio.s2.Domain.Amigos.Repository;
using Desafio.s2.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Desafios2.UnitTests.UnitTests
{
    public class AmigoControllerTestes
    {
        private AmigoAppService _mockedAmigoAppService;
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IUser> _mockedUser = new Mock<IUser>();
        private readonly Mock<IMediatorHandler> _mediatrHandler = new Mock<IMediatorHandler>();
        private readonly Mock<DomainNotificationHandler> _domainNotificationHandler = new Mock<DomainNotificationHandler>();
                
        [Fact]
        public void AmigoController_AdicionarAmigoComSucesso()
        {
            //Arrange
            var amigoViewModel = new AmigoViewModel();
            var registrarAmigoCommand = new RegistrarAmigoCommand("Amigo mocado", "email@mocado.com", Guid.NewGuid());

            _mapper.Setup(a => a.Map<RegistrarAmigoCommand>(amigoViewModel)).Returns(registrarAmigoCommand);

            _mockedAmigoAppService = new AmigoAppService(_mediatrHandler.Object, new Mock<IAmigoRepository>().Object, _mapper.Object, _mockedUser.Object);

            var amigoController = new AmigoController(
                _mockedAmigoAppService,
                _mockedUser.Object,
                _domainNotificationHandler.Object,
                _mediatrHandler.Object)
            {
               
                TempData = new TempDataDictionary(new Mock<HttpContext>().Object, new Mock<ITempDataProvider>().Object)
            };

            //Act
            var result = amigoController.Create(amigoViewModel);

            //Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Matches("Index", ((RedirectToActionResult)result).ActionName);
        }
    }
}