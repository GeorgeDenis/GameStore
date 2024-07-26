using Application;
using Application.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureRegisrationDI
    {
        public static IServiceCollection AddInfrastructureToDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SteamContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("SteamConnection"),
                    builder => builder.MigrationsAssembly(typeof(SteamContext).Assembly.FullName))
                );
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();


            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
