using Application.Persistence;
using Azure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Attributes
{
    public class AuthenticationAttribute : Attribute, IAsyncActionFilter
    {
        public AuthenticationAttribute()
        {

        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();

            var tokenData = tokenService.GetTokenData();
            if (!tokenData.IsSuccess)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            if (tokenData.Value.UserRole != "Admin")
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.HttpContext.Response.WriteAsync("You do not have permission to access this resource.");
                return;
            }
            await next();
            return;
        }
    }
}
