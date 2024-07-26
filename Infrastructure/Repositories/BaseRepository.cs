using Application.Persistence;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly SteamContext context;

        public BaseRepository(SteamContext context)
        {
            this.context = context;
        }

        public virtual async Task<Result<T>> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public virtual async Task<Result<T>> DeleteAsync(Guid id)
        {
            var entity = await FindByIdAsync(id);
            if (entity.IsSuccess)
            {
                context.Set<T>().Remove(entity.Value);
                return Result<T>.Success(entity.Value);
            }
            return Result<T>.Failure($"Entity with id {id} not found");
        }

        public virtual async Task<Result<T>> FindByIdAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if(entity == null)
            {
                return Result<T>.Failure($"Entity with id {id} not found");
            }
            return Result<T>.Success(entity);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetAllAsync()
        {
            var result = await context.Set<T>().AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetPagedResponseAsync(int page, int size)
        {
            var result = await context.Set<T>().Skip(page).Take(size).AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<T>> UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }
    }
}
