using System;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.EntityFramework.Mappings.SqlServer;
using OnlineStore.Entity.Concrete;

namespace OnlineStore.Data.EntityFramework.DbContextExtensions
{
    public static class MsSqlServerExtension
    {
        public static ModelBuilder AddMsSqlServerModelBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products", schema: "store");
            modelBuilder.Entity<Category>().ToTable("Categories", schema: "store");
            modelBuilder.Entity<User>().ToTable("Users", schema: "store");
            modelBuilder.Entity<Role>().ToTable("Roles", schema: "store");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles", schema: "store");
            modelBuilder.Entity<Log>().ToTable("Log", schema: "dbo");

            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new LogMap());

            return modelBuilder;
        }

        public static DbContextOptionsBuilder AddMsSqlServerOptionBuilder(this DbContextOptionsBuilder optionsBuilder,string connectionString)
        {
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder;
        }
    }
}
