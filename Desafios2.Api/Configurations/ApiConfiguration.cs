using System;
using Desafio.s2.Data.Context;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Desafio.s2.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Desafio.s2.Infra.CrossCutting.Identity.Authorization;

namespace Desafios2.Api.Configurations
{
    public static class ApiConfiguration
    {
        public static void AddApiSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var tokenConfigurations = new TokenDescriptor();

            new ConfigureFromConfigurationOptions<TokenDescriptor>(
                configuration.GetSection("JwtTokenOptions"))
                .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDataContext>()
            //    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;                
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;

                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = SigningCredentialsConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            //todo :  estudar claims para correta implementação
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("PodeLerEventos", policy => policy.RequireClaim("Eventos", "Ler"));
            //    options.AddPolicy("PodeGravar", policy => policy.RequireClaim("Eventos", "Gravar"));

            //    options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});
        }
    }
}