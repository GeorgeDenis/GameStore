using Application.Models.TokenInfo;
using Domain.Common;
using Domain.Entities;

namespace Application.Persistence
{
    public interface ITokenService
    {
        Result<string> GetUserId();
        Result<string> GetCurrentUserToken();
        Result<TokenInfo> GetTokenData();
    }
}
