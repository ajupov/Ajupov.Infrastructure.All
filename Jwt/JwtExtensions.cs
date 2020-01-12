using Ajupov.Infrastructure.All.Jwt.Helpers;
using Ajupov.Infrastructure.All.Jwt.JwtGenerator;
using Ajupov.Infrastructure.All.Jwt.JwtReader;
using Ajupov.Infrastructure.All.Jwt.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Ajupov.Infrastructure.All.Jwt
{
    public static class JwtExtensions
    {
        public static IServiceCollection ConfigureJwtGenerator(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"))
                .AddSingleton<IJwtGenerator, JwtGenerator.JwtGenerator>()
                .BuildServiceProvider();

            return services;
        }

        public static IServiceCollection ConfigureJwtReader(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IJwtReader, JwtReader.JwtReader>()
                .BuildServiceProvider();

            return services;
        }

        public static AuthenticationBuilder ConfigureJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "LiteCRM Identity";
            });
        }

        public static AuthenticationBuilder ConfigureJwtValidator(
            this AuthenticationBuilder builder,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            return builder
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                        ValidAudience = jwtSettings.GetValue<string>("Audience"),
                        IssuerSigningKey =
                            SymmetricSecurityKeyHelper.GetSymmetricSecurityKey(jwtSettings.GetValue<string>("Key"))
                    };
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}