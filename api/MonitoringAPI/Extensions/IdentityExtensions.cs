using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using MonitoringAPI.Services;

namespace MonitoringAPI.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddAuthentication(SetupAuthentication)
                .AddJwtBearer(options => SetupJwtBearer(options, configuration));

            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        private static void SetupAuthentication(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        public static void SetupJwtBearer(JwtBearerOptions options, IConfiguration configuration)
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JWTSettings:Issuer"],
                ValidAudience = configuration["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    context.NoResult();
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/plain";

                    return context.Response.WriteAsync(context.Exception.ToString());
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    return context.Response.WriteAsync(string.Empty);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";

                    return context.Response.WriteAsync(string.Empty);
                }
            };
        }
    }
}
