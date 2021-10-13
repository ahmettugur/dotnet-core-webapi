using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Entity.Concrete;

namespace OnlineStore.Data.EntityFramework.Mappings.MySql
{
    public class UserRoleMap: IEntityTypeConfiguration<UserRole>
    {

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).HasColumnName("id");
            builder.Property(_ => _.RoleId).HasColumnName("role_id");
            builder.Property(_ => _.UserId).HasColumnName("user_id");
        }
    }
}
