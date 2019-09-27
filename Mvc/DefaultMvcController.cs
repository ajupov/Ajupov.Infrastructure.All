using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.All.Mvc
{
    public class DefaultMvcController : Controller
    {
        protected string IpAddress => Request.HttpContext.Connection.RemoteIpAddress.ToString();

        protected string UserAgent => Request.Headers["User-Agent"].ToString();
    }
}