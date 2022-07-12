using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Base;

namespace Core.Data.Repository
{
    public interface IAsyncBaseRepository<T>
        where T : EntityBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> find);
        Task<T> FindAsync(int Id);
        Task<T> FindByAsync(Expression<Func<T, bool>> find);
        Task AddAsync(T Entity);
        Task AddRangeAsync(IEnumerable<T> Entities);
        Task DeleteAsync(int Id);
        Task DeleteRangeAsync(int[] Ids);
        Task DeleteByAsync(Expression<Func<T, bool>> find);
        Task UpdateAsync(T Entity);
        Task UpdateRangeAsync(IEnumerable<T> Entities);
        Task<bool> ExistAsync(Expression<Func<T, bool>> expression);
	Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }
}
