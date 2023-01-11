using BeSmart.Server.Domain.Interfaces;
using BeSmart.Server.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Server.Domain
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

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await context
                .Set<TEntity>()
                .ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await context
                .Set<TEntity>()
                .FindAsync(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await context
                .Set<TEntity>()
                .FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}