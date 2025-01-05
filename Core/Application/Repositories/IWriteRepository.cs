﻿using Domain.Common;

namespace Application.Repositories
{
    public interface IWriteRepository<T> where T :class, IBaseEntity, new()
    {
        Task AddAsync(T entity);
        Task AddRangeAsync (IList<T> entities);
        Task UpdateAsync (T entity);
        Task HardDeleteAsync (T entity);    
        Task SoftDeleteAsync (T entity);    
    }
}
