using MediatR;
using System.IO;
using AutoMapper;
using Desafio.s2.CrossCutting.IoC;
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

namespace Desafios2.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureFileUpload(services);
            ConfigureAspNetIdentityDbContext(services);
            ConfigureAspnetIdentityPassword(services);
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddMvc();
            services.AddAutoMapper();
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            BootStrapper.RegisterServices(services);
        }

        private static void ConfigureFileUpload(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        private void ConfigureAspNetIdentityDbContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        private static void ConfigureAspnetIdentityPassword(IServiceCollection services)
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