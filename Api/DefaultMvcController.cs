using Microsoft.AspNetCore.Mvc;

namespace Ajupov.Infrastructure.All.Api
{
    public class DefaultMvcController : Controller
    {
        protected string IpAddress => Request.Headers["X-Real-IP"].ToString();

        protected string UserAgent => Request.Headers["User-Agent"].ToString();
    }
}