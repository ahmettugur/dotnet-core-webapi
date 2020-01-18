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
            modelBuilder.Entity<Product>().ToTable("Products", schema: "dbo");
            modelBuilder.Entity<Category>().ToTable("Categories", schema: "dbo");
            modelBuilder.Entity<User>().ToTable("Users", schema: "dbo");
            modelBuilder.Entity<Role>().ToTable("Roles", schema: "dbo");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles", schema: "dbo");

            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());

            return modelBuilder;
        }

        public static DbContextOptionsBuilder AddMsSqlServerOptionBuilder(this DbContextOptionsBuilder optionsBuilder,string connectionString)
        {
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder;
        }
    }
}
