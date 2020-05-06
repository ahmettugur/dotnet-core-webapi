using System;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.EntityFramework.Mappings.PostgreSQL;
using OnlineStore.Entity.Concrete;

namespace OnlineStore.Data.EntityFramework.DbContextExtensions
{
    public static class SqlServerExtension
    {
        public static ModelBuilder AddPostgreSqlModelBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("products", schema: "public");
            modelBuilder.Entity<Category>().ToTable("categories", schema: "public");
            modelBuilder.Entity<User>().ToTable("users", schema: "public");
            modelBuilder.Entity<Role>().ToTable("roles", schema: "public");
            modelBuilder.Entity<UserRole>().ToTable("user_roles", schema: "public");
            modelBuilder.Entity<Log>().ToTable("log", schema: "public");

            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new LogMap());

            return modelBuilder;
        }

        public static DbContextOptionsBuilder AddPostgreSqlOptionBuilder(this DbContextOptionsBuilder optionsBuilder,string connectionString)
        {
            optionsBuilder.UseNpgsql(connectionString);

            return optionsBuilder;

        }
    }
}
