using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Entity.Concrete;


namespace OnlineStore.Data.EntityFramework.Mappings.MySql
{
    public class UserMap : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(_ => _.UserId);

            builder.Property(_ => _.UserId).HasColumnName("user_id");
            builder.Property(_ => _.FullName).HasColumnName("full_name");
            builder.Property(_ => _.Password).HasColumnName("password");
            builder.Property(_ => _.Email).HasColumnName("email");
        }
    }
}
