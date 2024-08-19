using Application.Models.TokenInfo;
using Azure.Core;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

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
            if (path.StartsWith("/api/v1/auth") || path.StartsWith("/notification"))
            {
                await next(context);
                return;
            }
            var token = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(token) && memoryCache.TryGetValue(token, out TokenInfo tokenInfo))
            {
                var currentFingerprint = TokenInfo.GetFingerprint(context.Request);
                if (tokenInfo.Fingerprint == currentFingerprint)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, tokenInfo.UserId),
                    new Claim(ClaimTypes.Email, tokenInfo.Email),
                    new Claim(ClaimTypes.Role, tokenInfo.UserRole)  // Role claim
                };

                    var identity = new ClaimsIdentity(claims, "Token");
                    context.User = new ClaimsPrincipal(identity);

                    var expirationTime = DateTime.Now.AddMinutes(30);
                    tokenInfo!.ExpirationTime = expirationTime;
                    memoryCache.Set(token, tokenInfo, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = expirationTime
                    });

                    await next(context);
                    return;
                }
            }
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}
