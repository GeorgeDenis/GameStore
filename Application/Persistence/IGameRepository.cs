using Domain.Common;
using Domain.Entities;

namespace Application.Persistence
{
    public interface IGameRepository : IAsyncRepository<Game>
    {
        Task<Result<Game>> FindByIdWithUsersAsync(Guid id);
    }
}
