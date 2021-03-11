using Microsoft.AspNetCore.Mvc;

namespace Ajupov.Infrastructure.All.Mvc
{
    public class DefaultMvcController : Controller
    {
        protected string IpAddress => Request.Headers["X-Real-IP"].ToString();

        protected string UserAgent => Request.Headers["User-Agent"].ToString();
    }
}