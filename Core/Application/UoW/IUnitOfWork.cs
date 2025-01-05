using Application.Repositories;
using Domain.Common;

namespace Application.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity, new();

        Task<int> SaveAsync();
        int Save();
    }
}
