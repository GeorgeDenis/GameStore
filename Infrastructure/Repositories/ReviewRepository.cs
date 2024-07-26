using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(SteamContext context) : base(context) { }

        public async Task<Result<List<Review>>> GetReviewsByGameId(Guid gameId)
        {
            var reviews = await context.Reviews.Where(x => x.GameId == gameId).ToListAsync();
            return Result<List<Review>>.Success(reviews);
        }
    }
}
