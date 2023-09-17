using NdbPortal.Web.Contracts;

namespace NdbPortal.Web.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IWebApiClient webApiClient)
        {
            var token = context.Session.GetString("JWToken");
            var employeeId = await webApiClient.ValidateTokenAsync(token);
            if (employeeId != null && token != null)
            {
                context.Items["User"] = await webApiClient.GetEmployeeByIdAsync(token, employeeId.Value);
            }

            await _next(context);
        }
    }
}
