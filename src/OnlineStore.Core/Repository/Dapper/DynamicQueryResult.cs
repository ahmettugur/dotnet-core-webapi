using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore.Core.Repository.Dapper
{
    public class DynamicQueryResult
    {
        public DynamicQueryResult(string sql, dynamic parameter)
        {
            this.Sql = sql;
            this.Parameter = parameter;
        }

        public string Sql { get; set; }

        public dynamic Parameter { get; set; }
    }
}
