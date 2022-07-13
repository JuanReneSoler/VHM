using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Core.Base;
using Microsoft.EntityFrameworkCore.Query;


namespace Core.Data.Repository
{
    public interface IBaseRepository<T>
        where T : EntityBase
    {
        IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        IEnumerable<T> GetBy(Expression<Func<T, bool>> find, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        T Find(int Id);
        T FindBy(Expression<Func<T, bool>> find, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        void Add(T Entity);
        void AddRange(IEnumerable<T> Entities);
        void Delete(int Id);
        void DeleteRange(int[] Ids);
        void DeleteBy(Expression<Func<T, bool>> find);
        void Update(T Entity);
        void UpdateRange(IEnumerable<T> Entities);
        bool Exist(Expression<Func<T, bool>> expression);
	int Count();
        int Count(Expression<Func<T, bool>> expression);
    }
}
