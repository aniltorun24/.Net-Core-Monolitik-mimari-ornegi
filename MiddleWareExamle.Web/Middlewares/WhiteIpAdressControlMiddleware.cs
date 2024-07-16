
using System.Net;

namespace MiddleWareExamle.Web.Middlewares
{
    public class WhiteIpAdressControlMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private const string whiteIpAdress = "::1";

        public WhiteIpAdressControlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //IPV4 =>127.0.0.1 =>localhost
            //IPV6 =>::1 =>localhost
            var reqIpAdress = context.Connection.RemoteIpAddress;
            bool anyWhiteIpAdress = IPAddress.Parse(whiteIpAdress).Equals(reqIpAdress);
            if (anyWhiteIpAdress)
            {
                await _requestDelegate(context);
            }
            else
            {
                context.Response.StatusCode = HttpStatusCode.Forbidden.GetHashCode();
                await context.Response.WriteAsync("Forbitten");
            }
        }
    }
}
