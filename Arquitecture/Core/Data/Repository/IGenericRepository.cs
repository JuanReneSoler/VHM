using System;
using Core.Base;

namespace Core.Data.Repository
{
    public interface IGenericRepository<T> : IBaseRepository<T>, IPaginateRepository<T>, 
        IAsyncRepository<T>, IDisposable
        where T : EntityBase
    {
    }
}
