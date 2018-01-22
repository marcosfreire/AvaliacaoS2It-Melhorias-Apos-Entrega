using MediatR;
using Desafio.s2.Data.Uow;
using Desafio.s2.Data.Context;
using Microsoft.AspNetCore.Http;
using Desafio.s2.Data.Repository;
using Desafio.s2.Domain.Interfaces;
using Desafio.s2.Data.EventSourcing;
using Desafio.s2.Domain.Jogos.Events;
using Desafio.s2.Domain.Core.Handlers;
using Desafio.s2.Domain.Jogos.Commands;
using Desafio.s2.Domain.Core.Interfaces;
using Desafio.s2.Domain.Jogos.Repository;
using Desafio.s2.Domain.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Desafio.s2.Data.Repository.EventSourcing;
using Desafio.s2.Infra.CrossCutting.Identity.Models;
using Desafio.s2.Infra.CrossCutting.Identity.Services;
using Desafio.s2.Domain.Amigos.Events;
using Desafio.s2.Domain.Amigos.Commands;
using Desafio.s2.Domain.Amigos.Repository;
using Desafio.s2.App.Service.Interfaces;
using Desafio.s2.App.Service.Service;
using AutoMapper;

namespace Desafio.s2.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            services.AddScoped<IJogoAppService, JogoAppService>();
            services.AddScoped<IAmigoAppService, AmigoAppService>();

            services.AddScoped<INotificationHandler<ExcluidJogoCommand>, JogoCommandHandler>();
            services.AddScoped<INotificationHandler<RegistrarJogoCommand>, JogoCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarJogoCommand>, JogoCommandHandler>();
            services.AddScoped<INotificationHandler<ExcluirAmigoCommand>, AmigoCommandHandler>();
            services.AddScoped<INotificationHandler<RegistrarAmigoCommand>, AmigoCommandHandler>();
            services.AddScoped<INotificationHandler<AtualizarAmigoCommand>, AmigoCommandHandler>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<JogoExcluidoEvent>, JogoEventHandler>();
            services.AddScoped<INotificationHandler<JogoRegistradoEvent>, JogoEventHandler>();
            services.AddScoped<INotificationHandler<JogoAtualizadoEvent>, JogoEventHandler>();
            services.AddScoped<INotificationHandler<AmigoExcluidoEvent>, AmigoEventHandler>();
            services.AddScoped<INotificationHandler<AmigoAtualizadoEvent>, AmigoEventHandler>();
            services.AddScoped<INotificationHandler<AmigoRegistradoEvent>, AmigoEventHandler>();

            services.AddScoped<IJogoRepository, JogoRepository>();
            services.AddScoped<IAmigoRepository, AmigoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ApplicationDataContext>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();
        }
    }
}