using BeSmart.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain
{
    public abstract class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
        where TContext : DbContext
    {
        private readonly TContext context;

        public RepositoryBase(TContext context)
        {
            this.context = context;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await context
                .Set<TEntity>()
                .ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await context
                .Set<TEntity>()
                .FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await context
                .Set<TEntity>()
                .FindAsync(id);

            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}