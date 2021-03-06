﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.OAuthClients
{
    public static class OAuthClientsExtensions
    {
        public static AuthenticationBuilder AddOAuthClients(
            this AuthenticationBuilder builder,
            IConfiguration configuration)
        {
            return builder
                .AddVkontakte(x =>
                {
                    var section = configuration.GetSection("VkontakteOauthClientSettings");

                    x.ClientId = section.GetValue<string>("ClientId");
                    x.ClientSecret = section.GetValue<string>("ClientSecret");
                    x.CallbackPath = "/VkontakteCallback";
                })
                .AddOdnoklassniki(x =>
                {
                    var section = configuration.GetSection("OdnoklassnikiOauthClientSettings");

                    x.ClientId = section.GetValue<string>("ClientId");
                    x.ClientSecret = section.GetValue<string>("ClientSecret");
                    x.CallbackPath = "/OdnoklassnikiCallback";
                    x.Scope.Add("VALUABLE_ACCESS");
                    x.Scope.Add("LONG_ACCESS_TOKEN");
                })
                .AddInstagram(x =>
                {
                    var section = configuration.GetSection("InstagramOauthClientSettings");

                    x.ClientId = section.GetValue<string>("ClientId");
                    x.ClientSecret = section.GetValue<string>("ClientSecret");
                    x.CallbackPath = "/InstagramCallback";
                })
                .AddYandex(x =>
                {
                    var section = configuration.GetSection("YandexOauthClientSettings");

                    x.ClientId = section.GetValue<string>("ClientId");
                    x.ClientSecret = section.GetValue<string>("ClientSecret");
                    x.CallbackPath = "/YandexCallback";
                })
                .AddMailRu(x =>
                {
                    var section = configuration.GetSection("MailRuOauthClientSettings");

                    x.ClientId = section.GetValue<string>("ClientId");
                    x.ClientSecret = section.GetValue<string>("ClientSecret");
                    x.CallbackPath = "/MailRuCallback";
                });
        }
    }
}