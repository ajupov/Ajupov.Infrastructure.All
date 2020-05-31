using Microsoft.AspNetCore.Mvc;

namespace Ajupov.Infrastructure.All.Mvc.Attributes
{
    public class RequestContentTypeApplicationJsonAttribute : ConsumesAttribute
    {
        public RequestContentTypeApplicationJsonAttribute()
            : base("application/json")
        {
        }
    }
}
