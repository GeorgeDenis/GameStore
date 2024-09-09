using Application;
using Application.Persistence;
using Application.Strategy;
using Hangfire;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Strategies;
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

            services.AddHangfire(config =>
                config.UseSqlServerStorage(
                        configuration.GetConnectionString("HangFireSteamConnectionString")));
            services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromSeconds(1));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();


            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerService<>));


            services.AddScoped<IPriceCalculator, PriceCalculator>();
            services.AddScoped<IPriceCalculationStrategy, RONPriceCalculationStrategy>();
            services.AddScoped<IPriceCalculationStrategy, USDPriceCalculationStrategy>();
            services.AddScoped<IPriceCalculationStrategy, EURPriceCalculationStrategy>();
            return services;
        }
    }
}
