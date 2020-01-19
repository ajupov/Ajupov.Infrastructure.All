using Ajupov.Infrastructure.All.Jwt.Helpers;
using Ajupov.Infrastructure.All.Jwt.JwtGenerator;
using Ajupov.Infrastructure.All.Jwt.JwtReader;
using Ajupov.Infrastructure.All.Jwt.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Ajupov.Infrastructure.All.Jwt
{
    public static class JwtExtensions
    {
        public static IServiceCollection ConfigureJwtGenerator(this IServiceCollection services)
        {
            return services
                .AddSingleton<IJwtGenerator, JwtGenerator.JwtGenerator>();
        }

        public static IServiceCollection ConfigureJwtReader(this IServiceCollection services)
        {
            return services
                .AddSingleton<IJwtReader, JwtReader.JwtReader>();
        }

        public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtDefaults.AuthenticationScheme;
            });
        }

        public static AuthenticationBuilder AddJwtValidator(
            this AuthenticationBuilder builder,
            IConfiguration configuration)
        {
            builder.Services
                .Configure<JwtValidatorSettings>(configuration.GetSection(nameof(JwtValidatorSettings)));

            return builder
                .AddJwtBearer(options =>
                {
                    var section = configuration.GetSection(nameof(JwtValidatorSettings));
                    var audience = section.GetValue<string>(nameof(JwtValidatorSettings.Audience));
                    var signingKey = section.GetValue<string>(nameof(JwtValidatorSettings.SigningKey));

                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = !string.IsNullOrWhiteSpace(audience),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtDefaults.AuthenticationScheme,
                        ValidAudience = audience,
                        IssuerSigningKey = SymmetricSecurityKeyHelper.GetSymmetricSecurityKey(signingKey)
                    };
                });
        }
    }
}