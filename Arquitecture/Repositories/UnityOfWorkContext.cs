using Core.Data.UnitOfWork;
using Persistence;

namespace Repositories;
public class UnityOfWorkContext : UnitOfWork<VHMContext>, IUnityOfWorkContext
{
    private readonly VHMContext _context;
    public UnityOfWorkContext(VHMContext context) : base(context)
    {
        this._context = context;
    }
}

