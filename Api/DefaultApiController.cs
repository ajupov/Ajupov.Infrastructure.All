using Microsoft.AspNetCore.Mvc;

namespace Ajupov.Infrastructure.All.Api
{
    public class DefaultApiController : ControllerBase
    {
        protected string IpAddress => Request.HttpContext.Connection.RemoteIpAddress?.ToString();

        protected string UserAgent => Request.Headers["User-Agent"].ToString();
    }
}
