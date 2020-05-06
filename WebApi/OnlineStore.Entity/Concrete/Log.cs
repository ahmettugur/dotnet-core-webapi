using System;
using OnlineStore.Core.Attributes;
using OnlineStore.Core.Contracts.Entities;

namespace OnlineStore.Entity.Concrete
{
    public class Log: IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string LogDetail { get; set; }

    }
}
