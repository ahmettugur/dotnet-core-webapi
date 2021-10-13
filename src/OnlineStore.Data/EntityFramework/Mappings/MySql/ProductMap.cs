using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Entity.Concrete;

namespace OnlineStore.Data.EntityFramework.Mappings.MySql
{
    public class ProductMap: IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).HasColumnName("id");
            builder.Property(_ => _.CategoryId).HasColumnName("category_id");
            builder.Property(_ => _.Name).HasColumnName("name");
            builder.Property(_ => _.Details).HasColumnName("details");
            builder.Property(_ => _.Price).HasColumnName("price");
            builder.Property(_ => _.StockQuantity).HasColumnName("stock_quantity");
        }
    }
}
