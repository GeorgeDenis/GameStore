using Domain.Common;
using Domain.Entities;

namespace Application.Persistence
{
    public interface IReviewRepository: IAsyncRepository<Review>
    {
        Task<Result<List<Review>>> GetReviewsByGameId(Guid gameId);
    }
}
