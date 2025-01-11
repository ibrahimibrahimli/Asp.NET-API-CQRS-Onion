using Application.Abstracts.Repositories;
using Domain.Common;

namespace Application.Abstracts.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity, new();

        Task<int> SaveAsync();
        int Save();
    }
}
