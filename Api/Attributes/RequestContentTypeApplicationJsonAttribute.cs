using Microsoft.AspNetCore.Mvc;

namespace Ajupov.Infrastructure.All.Api.Attributes
{
    public class RequestContentTypeApplicationJsonAttribute : ConsumesAttribute
    {
        public RequestContentTypeApplicationJsonAttribute()
            : base("application/json")
        {
        }
    }
}
