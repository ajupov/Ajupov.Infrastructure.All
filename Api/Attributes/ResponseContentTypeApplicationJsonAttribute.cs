using Microsoft.AspNetCore.Mvc;

namespace Ajupov.Infrastructure.All.Api.Attributes
{
    public class ResponseContentTypeApplicationJsonAttribute : ProducesAttribute
    {
        public ResponseContentTypeApplicationJsonAttribute()
            : base("application/json")
        {
        }
    }
}
