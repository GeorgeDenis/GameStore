using Application.Persistence;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class LoggerService<T> : IAppLogger<T> where T : class
    {
        private readonly ILogger<T> _logger;

        public LoggerService(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message, Exception ex)
        {
            _logger.LogError(ex, message);
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }
    }
}
