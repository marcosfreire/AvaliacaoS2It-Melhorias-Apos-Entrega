using MediatR;
using AutoMapper;
using Desafio.s2.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Desafio.s2.Infra.CrossCutting.Identity.Data;
using Desafio.s2.Infra.CrossCutting.Identity.Models;
using Desafio.s2.Infra.CrossCutting.Identity.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Desafio.s2.Site
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

        private static void ConfigureFileUpload(IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        private void ConfigureAspNetIdentityDbContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
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

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            BootStrapper.RegisterServices(services);
        }
    }
}