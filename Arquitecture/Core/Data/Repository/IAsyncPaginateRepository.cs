using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Data.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Core.Base;

namespace Core.Data.Repository
{
     public interface IAsyncPaginateRepository<T>
        where T : EntityBase
    {
        Task<IPaginate<T>> GetPaginatedListAsync(Expression<Func<T, bool>> predicate, int index, int size, 
            bool disableTracking = true);
        Task<IPaginate<T>> GetPaginatedListAsync(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int index, int size, bool disableTracking = true);
        Task<IPaginate<T>> GetPaginatedListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken));
        Task<IPaginate<TResult>> GetPaginatedListAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true,
            CancellationToken cancellationToken = default) where TResult : class;

    }
}
