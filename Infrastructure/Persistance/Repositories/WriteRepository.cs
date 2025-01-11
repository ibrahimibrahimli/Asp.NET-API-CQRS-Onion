using Application.Abstracts.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly ApplicationDbContext _context;

        public WriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private DbSet<T> Table { get => _context.Set<T>(); }

        public async Task AddAsync(T entity)
        {
                await Table.AddAsync(entity);
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task HardDeleteAsync(T entity)
        {
            await Task.Run(()=> Table.Remove(entity));
        }

        public async Task HardDeleteRangeAsync(IList<T> entity)
        {
           await Task.Run(() => Table.RemoveRange(entity));
        }

        public Task SoftDeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {   
            await Task.Run(() => Table.Update(entity));
        }
    }
}
