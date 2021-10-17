using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineStore.Data.EntityFramework.Mappings.PostgreSQL;
using OnlineStore.Data.EntityFramework.DbContextExtensions;
using OnlineStore.Entity.Concrete;
using System.IO;

namespace OnlineStore.Data.EntityFramework
{
    public class OnlineStoreContext : DbContext
    {
        public IConfigurationRoot Configuration { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string connectionString = Configuration.GetConnectionString("OnlineStoreContext");

            optionsBuilder.AddMsSqlServerOptionBuilder(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddMsSqlServerModelBuilder();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
