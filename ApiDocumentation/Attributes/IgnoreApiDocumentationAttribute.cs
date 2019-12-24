using System;
using Microsoft.AspNetCore.Mvc;

namespace Ajupov.Infrastructure.All.ApiDocumentation.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IgnoreApiDocumentationAttribute : ApiExplorerSettingsAttribute
    {
        public IgnoreApiDocumentationAttribute()
        {
            IgnoreApi = true;
        }
    }
}