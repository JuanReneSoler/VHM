using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }

    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}







