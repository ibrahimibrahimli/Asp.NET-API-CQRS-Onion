using Domain.Common;

namespace Application.Abstracts.Repositories
{
    public interface IReadRepository<T> where T : BaseAuditableEntity
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        IQueryable<T> Query();
    }
}
