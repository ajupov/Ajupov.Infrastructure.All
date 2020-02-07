using Microsoft.AspNetCore.Mvc;

namespace Ajupov.Infrastructure.All.Mvc.Attributes
{
    public class ResponseContentTypeApplicationJsonAttribute : ProducesAttribute
    {
        public ResponseContentTypeApplicationJsonAttribute() : base("application/json")
        {
        }
    }
}