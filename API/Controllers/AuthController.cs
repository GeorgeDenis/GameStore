using API.Models;
using Application.Models.TokenInfo;
using Application.Models.User;
using Application.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IAuthenticationService authenticationService;
        public AuthController(IMemoryCache memoryCache, IAuthenticationService authenticationService)
        {
            this.memoryCache = memoryCache;
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await authenticationService.Login(model.Email, model.Password);
            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
            return Ok(new { Token = result.Value.Token, Role = result.Value.Role, Expiration = result.Value.ExpirationTime });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(PostUserModel model)
        {
            var result = await authenticationService.Register(model.Name, model.Email, model.Password);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok();
        }

        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token) && memoryCache.TryGetValue(token, out TokenInfo tokenInfo))
            {
                return Ok(new { Valid = true, UserId = tokenInfo.UserId, ExpirationTime = tokenInfo.ExpirationTime });
            }
            return Unauthorized(new { Valid = true, Message = "Token expired" });
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            authenticationService.Logout();
            return Ok();
        }


    }
}
