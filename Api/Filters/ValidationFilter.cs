﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ajupov.Infrastructure.All.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                return next();
            }

            context.Result = new BadRequestObjectResult(context.ModelState);

            return Task.CompletedTask;
        }
    }
}
