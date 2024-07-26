using Application.Models.Authentication;
using Application.Models.TokenInfo;
using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        private readonly IMemoryCache memoryCache;

        public AuthenticationService(IMemoryCache memoryCache, IUserRepository userRepository, ITokenService tokenService)
        {
            this.memoryCache = memoryCache;
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }
        public async Task<Result<LoginResponse>> Login(string email, string password)
        {
            var user = await userRepository.FindByEmailAsync(email);
            if (!user.IsSuccess)
            {
                return Result<LoginResponse>.Failure("User not found");
            }
            if (email == user.Value.Email && BCrypt.Net.BCrypt.Verify(password, user.Value.Password))
            {
                var token = Guid.NewGuid().ToString();
                var expirationTime = DateTime.Now.AddMinutes(30);

                var tokenInfo = new TokenInfo
                {
                    UserId = user.Value.UserId.ToString(),
                    Email = email,
                    ExpirationTime = expirationTime
                };

                memoryCache.Set(token, tokenInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = expirationTime
                });
                var loginResponse = new LoginResponse
                {
                    UserId = user.Value.UserId,
                    Name = user.Value.Name,
                    Email = email,
                    Password = password,
                    Token = token,
                    ExpirationTime = expirationTime
                };
                return Result<LoginResponse>.Success(loginResponse);
            }
            else
            {
                return Result<LoginResponse>.Failure("Wrong credentials");
            }
        }

        public Task Logout()
        {
            var token = tokenService.GetCurrentUserToken().Value;
            memoryCache.Remove(token);
            return Task.CompletedTask;
        }

        public async Task<Result<User>> Register(string name, string email, string password)
        {
            var userByEmail = await userRepository.FindByEmailAsync(email);
            if (userByEmail.IsSuccess)
            {
                return Result<User>.Failure("Email already in use");
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User(name, email, hashedPassword);
            var result = await userRepository.AddAsync(user);
            if (!result.IsSuccess)
            {
                return Result<User>.Failure("Something went wrong while creating the user");
            }
            return Result<User>.Success(result.Value);
        }
    }
}
