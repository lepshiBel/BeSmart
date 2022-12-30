using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(int id);
    }
}