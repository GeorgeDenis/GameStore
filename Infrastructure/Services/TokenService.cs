using Application.Models.TokenInfo;
using Application.Persistence;
using Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMemoryCache memoryCache;


        public TokenService(IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.memoryCache = memoryCache;
        }

        public Result<string> GetCurrentUserToken()
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            return Result<string>.Success(token);
        }

        public Result<string> GetUserId()
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(token))
            {
                return Result<string>.Failure("Authorization token is missing!");
            }
            if (!memoryCache.TryGetValue(token, out TokenInfo tokenData))
            {
                return Result<string>.Failure("Token info not found in cache!");
            }

            return Result<string>.Success(tokenData.UserId);

        }
    }
}
