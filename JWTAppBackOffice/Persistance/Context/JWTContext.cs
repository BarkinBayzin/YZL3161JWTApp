using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Persistance.Configurations.AppUserConfigurations;
using JWTAppBackOffice.Persistance.Configurations.ProductConfigurations;
using Microsoft.EntityFrameworkCore;

namespace JWTAppBackOffice.Persistance.Context
{
    public class JWTContext : DbContext
    {
        public JWTContext(DbContextOptions<JWTContext> options): base(options) { }
        public DbSet<Product> Products
        {
            get => this.Set<Product>();
        }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<AppRole> AppRoles => Set<AppRole>();
        public DbSet<Supplier> Supplier => Set<Supplier>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
