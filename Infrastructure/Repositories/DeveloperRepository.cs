using Application.Persistence;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class DeveloperRepository : BaseRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(SteamContext context) : base(context) { }
    }
}
