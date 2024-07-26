using Application.Models.Authentication;
using Application.Models.User;
using Domain.Common;
using Domain.Entities;

namespace Application.Persistence
{
    public interface IAuthenticationService
    {
        Task<Result<LoginResponse>> Login(string email, string password);
        Task<Result<User>> Register(string name, string email, string password);

        Task Logout();

    }
}
