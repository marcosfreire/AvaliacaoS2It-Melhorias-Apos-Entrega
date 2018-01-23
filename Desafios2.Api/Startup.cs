using MediatR;
using System.IO;
using AutoMapper;
using Desafio.s2.CrossCutting.IoC;
using Desafios2.Api.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Desafio.s2.Infra.CrossCutting.Identity.Data;
using Desafio.s2.Infra.CrossCutting.Identity.Models;
using Desafio.s2.Infra.CrossCutting.Identity.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace Desafios2.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurarFileUpload(services);
            RegistrarApplicationDbContext(services);
            ConfigurarComplexidadeSenhaAspnetIdentity(services);
            ConfigurarJwtToken(services);
            RegistrarConfiguracaoEmail(services);
            RegistrarOptions(services);
            RegistrarMvc(services);
            RegistrarAutoMapper(services);
            RegisterServices(services);
        }

        private static void RegistrarOptions(IServiceCollection services)
        {
            services.AddOptions();
        }

        private static void RegistrarAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper();
        }

        private static void RegistrarMvc(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());                
            });

            services.AddMvc();
        }

        private void RegistrarConfiguracaoEmail(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
        }

        private void ConfigurarJwtToken(IServiceCollection services)
        {
            services.AddApiSecurity(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            BootStrapper.RegisterServices(services);
        }

        private static void ConfigurarFileUpload(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        private void RegistrarApplicationDbContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        private static void ConfigurarComplexidadeSenhaAspnetIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }
    }
}