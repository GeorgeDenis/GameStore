using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(SteamContext context) : base(context) { }

        public async Task<Result<Game>> FindByIdWithUsersAsync(Guid id)
        {
            var game = await context.Games.Include(c => c.Users).FirstOrDefaultAsync(c => c.GameId == id);
            if (game != null)
            {
                return Result<Game>.Success(game);
            }
            return Result<Game>.Failure($"Entity with id {id} not found");
        }
    }
}
