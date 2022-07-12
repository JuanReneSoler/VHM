using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork
     where TContext : DbContext
    {
        private readonly TContext _context;
        public UnitOfWork(TContext context)
        {
            this._context = context;
        }

        public void Commit()
        {
            this._context.SaveChanges();
            this._context.Database.BeginTransaction().Commit();
        }

        public async Task CommitAsync()
        {
            await this._context.SaveChangesAsync();
            await this._context.Database.BeginTransaction().RollbackAsync();
        }

        public void Rollback()=>this._context.Database.BeginTransaction().Rollback();

        public async Task RollbackAsync()=> await this._context.Database.BeginTransaction().RollbackAsync();

        #region Dispose
        private bool _disposed = false;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                    this._context.Dispose();
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}


