using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Entity.Concrete;

namespace OnlineStore.Data.EntityFramework.Mappings.MySql
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).HasColumnName("id");
            builder.Property(_ => _.CreateDate).HasColumnName("create_date");
            builder.Property(_ => _.LogDetail).HasColumnName("log_detail");
        }
    }
}
