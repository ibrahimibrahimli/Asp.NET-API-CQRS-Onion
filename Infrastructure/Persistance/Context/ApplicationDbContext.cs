    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    namespace Persistance.Context
    {
        public class ApplicationDbContext : DbContext
        {

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Brand> Brands { get; set; }
            public DbSet<Detail> Details { get; set; }
            public DbSet<Product> Products { get; set; }    
            public DbSet<Category> Categories { get; set; }

            override protected void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            }
        }
    }
