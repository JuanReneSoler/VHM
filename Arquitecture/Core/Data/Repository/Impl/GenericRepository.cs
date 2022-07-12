using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Data.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Core.Base;

namespace Core.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : EntityBase
    {
	private DbContext _context;
        public DbSet<T> EntityBase => this._context.Set<T>();
        public GenericRepository(DbContext context)
        {
            this._context = context;
        }
        #region IBaseRepository
	public IEnumerable<T> GetAll() => EntityBase.ToList();
        public IEnumerable<T> GetBy(Expression<Func<T, bool>> find) => EntityBase.Where(find).ToList();
        public T Find(int Id) => EntityBase.Where(x => x.Id == Id).Single();
        public T FindBy(Expression<Func<T, bool>> find) => EntityBase.Where(find).Single();
        public void Add(T Entity) => EntityBase.Add(Entity);
        public void AddRange(IEnumerable<T> Entities) => EntityBase.AddRange(Entities);
        public void Delete(int Id)
	{
	    var entity = Find(Id);
	    EntityBase.Remove(entity);
	}
        public void DeleteRange(int[] Ids)
	{
	    var entities = EntityBase.Where(x => Ids.Any(y => y == x.Id));
	    EntityBase.RemoveRange(entities);
	}
        public void DeleteBy(Expression<Func<T, bool>> find)
	{
	    var entityes = EntityBase.Where(find).ToList();
	    EntityBase.RemoveRange(entityes);
	}
        public void Update(T Entity)=>EntityBase.Update(Entity);
        public void UpdateRange(IEnumerable<T> Entities)=>EntityBase.UpdateRange(Entities);
        public bool Exist(Expression<Func<T, bool>> expression)=>EntityBase.Any(expression);
	public int Count()=>EntityBase.Count();
        public int Count(Expression<Func<T, bool>> expression)=>EntityBase.Count(expression);
        #endregion
        #region IAsyncRepository
        public async Task<IEnumerable<T>> GetAllAsync() => await Task.Run(()=> GetAll());
        public async Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> find) => await Task.Run(()=> GetBy(find));
        public async Task<T> FindAsync(int Id) => await Task.Run(()=> Find(Id));
        public async Task<T> FindByAsync(Expression<Func<T, bool>> find) => await Task.Run(()=>FindBy(find));
        public async Task AddAsync(T Entity) => await Task.Run(()=> Add(Entity));
        public async Task AddRangeAsync(IEnumerable<T> Entities) => await Task.Run(()=> AddRange(Entities));
        public async Task DeleteAsync(int Id) => await Task.Run(() => Delete(Id));
        public async Task DeleteRangeAsync(int[] Ids) => await Task.Run(() => DeleteRange(Ids));
        public async Task DeleteByAsync(Expression<Func<T, bool>> find) => await Task.Run(() => DeleteBy(find));
        public async Task UpdateAsync(T Entity) => await Task.Run(() => Update(Entity));
        public async Task UpdateRangeAsync(IEnumerable<T> Entities) => await Task.Run(() => UpdateRange(Entities));
        public async Task<bool> ExistAsync(Expression<Func<T, bool>> expression) => await Task.Run(() => Exist(expression));
	public async Task<int> CountAsync() => await Task.Run(() => Count());
        public async Task<int> CountAsync(Expression<Func<T, bool>> expression) => await Task.Run(() => Count(expression));
        #endregion
        #region IPaginateRepository
        public IPaginate<T> GetPaginatedList(Expression<Func<T, bool>> predicate, int index, int size, 
            bool disableTracking = true)=>this.GetPaginatedList(predicate, null, index, size, disableTracking);
        public IPaginate<T> GetPaginatedList(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int index, int size, bool disableTracking = true)
            {
                IQueryable<T> query = EntityBase;
                if (disableTracking) query = query.AsNoTracking();

                if (predicate != null) query = query.Where(predicate);

                return orderBy != null ? orderBy(query).ToSimplePaginate(index, size) : query.ToSimplePaginate(index, size);
            }
        public IPaginate<T> GetPaginatedList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true)
            {
                IQueryable<T> query = this.EntityBase;
                if (disableTracking) query = query.AsNoTracking();

                if (include != null) query = include(query);

                if (predicate != null) query = query.Where(predicate);

                return orderBy != null
                    ? orderBy(query).ToSimplePaginate(index, size, 0)
                    : query.ToSimplePaginate(index, size, 0);

            }
        public IPaginate<TResult> GetPaginatedList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true) where TResult : class
            {
                IQueryable<T> query = EntityBase;
                if (disableTracking) query = query.AsNoTracking();

                if (include != null) query = include(query);

                if (predicate != null) query = query.Where(predicate);

                return orderBy != null
                ? orderBy(query).Select(selector).ToSimplePaginate(index, size, 0)
                : query.Select(selector).ToSimplePaginate(index, size, 0);
            }
        #endregion
        #region IAsyncPaginateRepository
        public async Task<IPaginate<T>> GetPaginatedListAsync(Expression<Func<T, bool>> predicate, int index, int size, 
            bool disableTracking = true) => await Task.Run(()=>this.GetPaginatedList(predicate, index, size, disableTracking));
        public async Task<IPaginate<T>> GetPaginatedListAsync(Expression<Func<T, bool>> predicate, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int index, int size, bool disableTracking = true)=>await Task.Run(()=>this.GetPaginatedList(predicate, orderBy, index, size, disableTracking));
        public async Task<IPaginate<T>> GetPaginatedListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken))=>await Task.Run(()=>
                this.GetPaginatedList(predicate, orderBy, include, index, size, disableTracking));
        public async Task<IPaginate<TResult>> GetPaginatedListAsync<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0,
            int size = 10,
            bool disableTracking = true,
            CancellationToken cancellationToken = default) where TResult : class
            =>await Task.Run(()=>this.GetPaginatedList(selector, predicate, orderBy, include, index, size, disableTracking));
        #endregion
        #region Dispose
        private bool _disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
        #endregion
    }
}
