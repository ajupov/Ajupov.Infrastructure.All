using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Ajupov.Infrastructure.All.Cookies
{
    public static class CookieExtensions
    {
        public static AuthenticationBuilder AddCookieDefaults(this AuthenticationBuilder builder)
        {
            return builder.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static IServiceCollection AddCookiePolicy(this IServiceCollection services)
        {
            return services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = x => CheckSameSite(x.Context, x.CookieOptions);
                options.OnDeleteCookie = x => CheckSameSite(x.Context, x.CookieOptions);
            });
        }

        private static void CheckSameSite(HttpContext context, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None &&
                context.Request.Headers["User-Agent"].ToString().Contains("Chrome"))
            {
                options.SameSite = SameSiteMode.Unspecified;
            }
        }
    }
}
