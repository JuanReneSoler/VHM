using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Data.Repository;
using Core.Base;

namespace Core.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
	IGenericRepository<T> GetRepository<T>() where T : EntityBase; 
    }

    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}







