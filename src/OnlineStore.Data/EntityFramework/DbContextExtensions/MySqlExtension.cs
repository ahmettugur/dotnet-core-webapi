using System;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.EntityFramework.Mappings.MySql;
using OnlineStore.Entity.Concrete;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace OnlineStore.Data.EntityFramework.DbContextExtensions
{
    public static class MySqlExtension
    {
        public static ModelBuilder AddMySqlModelBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<UserRole>().ToTable("user_roles");
            modelBuilder.Entity<Log>().ToTable("log");

            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new LogMap());

            return modelBuilder;
        }

        public static DbContextOptionsBuilder AddMySqlOptionBuilder(this DbContextOptionsBuilder optionsBuilder,string connectionString)
        {
            optionsBuilder.UseMySql(connectionString,new MySqlServerVersion(new Version(8, 0, 18)));
            
            
            // optionsBuilder.UseMySql(connectionString, mySqlOptions => mySqlOptions
            //         .ServerVersion(new ServerVersion(new Version(8, 0, 18), ServerType.MySql)));
            return optionsBuilder;
        }
    }
}
