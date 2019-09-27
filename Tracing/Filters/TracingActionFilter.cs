using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using OpenTracing;

namespace Infrastructure.All.Tracing.Filters
{
    public class TracingActionFilter : IAsyncActionFilter
    {
        private readonly ITracer _tracer;

        public TracingActionFilter(ITracer tracer)
        {
            _tracer = tracer;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using var scope = _tracer.BuildSpan(context.HttpContext.Request.Path).StartActive(true);
            await next();
        }
    }
}