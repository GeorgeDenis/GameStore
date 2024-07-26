using Application.Models.TokenInfo;
using Microsoft.Extensions.Caching.Memory;

namespace API.Middleware
{
    public class TokenMiddleware : IMiddleware
    {
        private readonly IMemoryCache memoryCache;


        public TokenMiddleware(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var path = context.Request.Path.ToString().ToLower();
            if (path.StartsWith("/api/v1/auth"))
            {
                await next(context);
                return;
            }
            var token = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(token) && memoryCache.TryGetValue(token, out TokenInfo tokenInfo))
            {
                var expirationTime = DateTime.Now.AddMinutes(30);
                tokenInfo!.ExpirationTime = expirationTime;
                memoryCache.Set(token, tokenInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = expirationTime
                });
                await next(context);
                return;
            }
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}
