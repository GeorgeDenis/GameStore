using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SteamContext context) : base(context) { }

        public async Task<Result<User>> FindByIdWithGamesAsync(Guid id)
        {
            var user = await context.Users.Include(c => c.Games).FirstOrDefaultAsync(c => c.UserId == id);
            if (user != null)
            {
                return Result<User>.Success(user);
            }
            return Result<User>.Failure($"Entity with id {id} not found");
        }

        public async Task<Result<User>> FindByEmailAsync(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(c => c.Email == email);
            if (user != null)
            {
                return Result<User>.Success(user);
            }
            return Result<User>.Failure($"Entity with id {email} not found");
        }

        public async Task<IEnumerable<Guid>> GetAllUserIds()
        {
            var userIds = new List<Guid>();
            var users = context.Users.ToList();
            foreach (var user in users)
            {
                userIds.Add(user.UserId);
            }
            return userIds;
        }
    }
}
