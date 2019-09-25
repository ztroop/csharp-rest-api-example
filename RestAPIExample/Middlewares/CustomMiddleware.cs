using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RestAPIExample.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MyConfiguration _myconfig;

        public CustomMiddleware(RequestDelegate next, IOptions<MyConfiguration> myconfig)
        {
            _next = next;
            _myconfig = myconfig.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Debug.WriteLine($" ---> Request asked for {httpContext.Request.Path} from {_myconfig.username}");
            // Call the next middleware delegate in the pipeline.
            await _next.Invoke(httpContext);
        }
    }
}
