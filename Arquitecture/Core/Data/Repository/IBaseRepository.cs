using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Core.Base;

namespace Core.Data.Repository
{
    public interface IBaseRepository<T>
        where T : EntityBase
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBy(Expression<Func<T, bool>> find);
        T Find(int Id);
        T FindBy(Expression<Func<T, bool>> find);
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
