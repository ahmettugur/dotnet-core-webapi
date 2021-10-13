using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Entity.Concrete;


namespace OnlineStore.Data.EntityFramework.Mappings.MySql
{
    public class CategoryMap: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).HasColumnName("id");
            builder.Property(_ => _.Name).HasColumnName("name");
            builder.Property(_ => _.Description).HasColumnName("description");
        }
    }
}
