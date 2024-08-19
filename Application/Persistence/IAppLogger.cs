namespace Application.Persistence
{
    public interface IAppLogger<T> where T : class
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
        void LogDebug(string message);
    }
}
