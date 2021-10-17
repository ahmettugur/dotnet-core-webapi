using Microsoft.Extensions.Configuration;
using OnlineStore.Core.Repository.Dapper;
using OnlineStore.Data.Contracts;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace OnlineStore.Data.Dapper
{
    public class DapperLogRepository : DapperGenericRepository<Category>, ICategoryRepository
    {
        private readonly SqlDbConnection _connection;
        public DapperLogRepository()
        {
            _connection = new SqlDbConnection();
        }

        protected override IDbConnection Connection => _connection.GetSqlServerConnection();

        protected override string TableName => "Log";
    }
}
