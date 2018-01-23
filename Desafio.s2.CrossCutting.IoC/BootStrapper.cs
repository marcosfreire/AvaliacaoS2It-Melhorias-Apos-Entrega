using MediatR;
using AutoMapper;
using Desafio.s2.Data.Uow;
using Desafio.s2.Data.Context;
using Microsoft.AspNetCore.Http;
using Desafio.s2.Data.Repository;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.Data.EventSourcing;
using Desafio.s2.App.Service.Service;
using Desafio.s2.Domain.Jogos.Events;
using Desafio.s2.Domain.Amigos.Events;
using Desafio.s2.Domain.Core.Handlers;
using Desafio.s2.Domain.Jogos.Commands;
using Desafio.s2.Domain.Amigos.Commands;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Jogos.Repository;
using Desafio.s2.Domain.Amigos.Repository;
using Desafio.s2.Domain.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Desafio.s2.Data.Repository.EventSourcing;
using Desafio.s2.Infra.CrossCutting.Identity.Models;
using Desafio.s2.Infra.CrossCutting.Identity.Services;

namespace Desafio.s2.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            RegistrarInterfacesService(services);
            RegistrarCommands(services);
            RegistrarEvents(services);
            RegistrarInterfacesRepositorio(services);
            RegistrarMediatrHandler(services);
            RegistrarEmailSender(services);
            RegistrarIUserInterface(services);
            RegistrarEventSourcingInterfaces(services);
        }

        private static void RegistrarEventSourcingInterfaces(IServiceCollection services)
        {
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();
        }

        private static void RegistrarIUserInterface(IServiceCollection services)
        {
            services.AddScoped<IUser, AspNetUser>();
        }

        private static void RegistrarEmailSender(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
        }

        private static void RegistrarMediatrHandler(IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }

        private static void RegistrarInterfacesRepositorio(IServiceCollection services)
        {
            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IAmigoRepository, AmigoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ApplicationDataContext>();
        }

        private static void RegistrarEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<JogoExcluidoEvent>, JogoEventHandler>();
            services.AddScoped<INotificationHandler<JogoRegistradoEvent>, JogoEventHandler>();
            services.AddScoped<INotificationHandler<JogoAtualizadoEvent>, JogoEventHandler>();
            services.AddScoped<INotificationHandler<AmigoExcluidoEvent>, AmigoEventHandler>();
            services.AddScoped<INotificationHandler<AmigoAtualizadoEvent>, AmigoEventHandler>();
            services.AddScoped<INotificationHandler<AmigoRegistradoEvent>, AmigoEventHandler>();
        }

        private static void RegistrarCommands(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<ExcluidJogoCommand>, JogoCommandHandler>();
            services.AddScoped<INotificationHandler<RegistrarJogoCommand>, JogoCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarJogoCommand>, JogoCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirAmigoCommand>, AmigoCommandHandler>();
            services.AddScoped<INotificationHandler<RegistrarAmigoCommand>, AmigoCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarAmigoCommand>, AmigoCommandHandler>();
        }

        private static void RegistrarInterfacesService(IServiceCollection services)
        {
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IJogoAppService, JogoAppService>();
            services.AddScoped<IAmigoAppService, AmigoAppService>();
        }
    }
}