using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Entity.Concrete;

namespace OnlineStore.Data.EntityFramework.Mappings.SqlServer
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Id).HasColumnName("Id");
            builder.Property(_ => _.CreateDate).HasColumnName("CreateDate");
            builder.Property(_ => _.LogDetail).HasColumnName("LogDetail");
            builder.Property(_ => _.LogName).HasColumnName("LogName");
        }
    }
}
