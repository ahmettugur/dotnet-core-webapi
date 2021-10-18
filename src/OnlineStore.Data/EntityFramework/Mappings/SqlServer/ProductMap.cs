using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Entity.Concrete;

namespace OnlineStore.Data.EntityFramework.Mappings.SqlServer
{
    public class ProductMap: IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.CategoryId);
            builder.Property(_ => _.Name);
            builder.Property(_ => _.Details);
            builder.Property(_ => _.Price).HasColumnType("decimal(18,2)");
            builder.Property(_ => _.StockQuantity);
        }
    }
}
