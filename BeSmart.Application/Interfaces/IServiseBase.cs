using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Interfaces
{
    public interface IServiseBase<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        void FindById(int Id);
        void Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
