    using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    namespace Persistance.Context
    {
        public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
        {

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<Brand> Brands { get; set; }
            public DbSet<Detail> Details { get; set; }
            public DbSet<Product> Products { get; set; }    
            public DbSet<Category> Categories { get; set; }
            public DbSet<ProductCategory> ProductCategories { get; set; }

            override protected void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            }
        }
    }
