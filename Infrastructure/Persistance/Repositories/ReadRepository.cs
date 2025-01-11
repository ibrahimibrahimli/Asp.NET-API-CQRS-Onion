using Application.Abstracts.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistance.Context;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly ApplicationDbContext _context;

        public ReadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //private DbSet<T> Table { get => _context.Set<T>(); }

        private DbSet<T> Table
        {
            get
            {
                if (_context == null)
                {
                    Console.WriteLine("context null");
                }

                var dbSet = _context.Set<T>();
                if (dbSet == null)
                {
                    Console.WriteLine("db set null");
                }

                return dbSet;
            }
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if(!enableTracking)
                queryable = queryable.AsNoTracking();
            if(include is not null)
                queryable = include(queryable);
            if (predicate is not null)
                queryable = queryable.Where(predicate);
            if(orderBy is not null)
                return await orderBy(queryable).ToListAsync();
            return await queryable.ToListAsync();
        }

        public async Task<List<T>> GetAllByPaginationAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currentPage = 1, int pageSize = 3)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            if (predicate is not null)
                queryable = queryable.Where(predicate);
            if (orderBy is not null)
                return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);

            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            if (!enableTracking)
                Table.AsNoTracking();

            return Table.Where(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            Table.AsNoTracking();
            if (predicate is not null)
                //return await _entities.Where(predicate).CountAsync();
                Table.Where(predicate);

            return await Table.CountAsync();
        }
    }
}
