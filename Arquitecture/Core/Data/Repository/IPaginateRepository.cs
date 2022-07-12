using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Core.Data.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Core.Base;

namespace Core.Data.Repository
{
    public interface IPaginateRepository<T>
        where T : EntityBase
    {
        IPaginate<T> GetPaginatedList(Expression<Func<T, bool>> predicate, int index, int size, 
            bool disableTracking = true);
        IPaginate<T> GetPaginatedList(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int index, int size, bool disableTracking = true);
        IPaginate<T> GetPaginatedList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true);
        IPaginate<TResult> GetPaginatedList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true) where TResult : class;
    }
}
