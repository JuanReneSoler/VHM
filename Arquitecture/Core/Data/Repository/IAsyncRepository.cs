using Core.Base;
namespace Core.Data.Repository
{
    public interface IAsyncRepository<T> : IAsyncBaseRepository<T>, IAsyncPaginateRepository<T>
        where T : EntityBase
    {
    }
}
