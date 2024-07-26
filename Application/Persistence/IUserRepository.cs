using Domain.Common;
using Domain.Entities;

namespace Application.Persistence
{
    public interface IUserRepository: IAsyncRepository<User>
    {
        Task<Result<User>> FindByIdWithGamesAsync(Guid id);
        Task<Result<User>> FindByEmailAsync(string username);
    }
}
