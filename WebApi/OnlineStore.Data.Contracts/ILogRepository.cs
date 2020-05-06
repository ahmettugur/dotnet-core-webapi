using OnlineStore.Core.Contracts.Repository;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.Contracts
{
    public interface ILogRepository : IGenericRepository<Log>
    {
    }
}
