using System;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.EntityFramework.Mappings.MySqlAndPostgreSQL;
using OnlineStore.Entity.Concrete;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace OnlineStore.Data.EntityFramework.DbContextExtensions
{
    public static class MySqlAndPostreSqlExtension
    {
        public static ModelBuilder AddMySqlModelBuilder(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("products","store");
            modelBuilder.Entity<Category>().ToTable("categories", "store");
            modelBuilder.Entity<User>().ToTable("users","store");
            modelBuilder.Entity<Role>().ToTable("roles", "store");
            modelBuilder.Entity<UserRole>().ToTable("user_roles", "store");
            modelBuilder.Entity<Log>().ToTable("log", "store");

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
            return optionsBuilder;
        }

        public static DbContextOptionsBuilder AddPostgreSqlOptionBuilder(this DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder.UseNpgsql(connectionString);

            return optionsBuilder;

        }
    }
}
