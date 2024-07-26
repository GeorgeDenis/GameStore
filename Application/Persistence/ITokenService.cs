using Domain.Common;

namespace Application.Persistence
{
    public interface ITokenService
    {
        Result<string> GetUserId();
        Result<string> GetCurrentUserToken();
    }
}
