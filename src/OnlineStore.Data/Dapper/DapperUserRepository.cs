using Dapper;
using OnlineStore.Core.Repository.Dapper;
using OnlineStore.Data.Contracts;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OnlineStore.Data.Dapper
{
    public class DapperUserRepository : DapperGenericRepository<User>, IUserRespository
    {
        private readonly SqlDbConnection _connection;
        public DapperUserRepository()
        {
            _connection = new SqlDbConnection();
        }

        protected override IDbConnection Connection => _connection.GetSqlServerConnection();

        protected override string TableName => "Users";

        public string[] GetUserRoles(User user)
        {
            using var conn = Connection;
            var sql = "SELECT r.Id,r.Name FROM Roles r " +
                         "INNER JOIN UserRoles ur ON r.Id = ur.RoleId " +
                         "WHERE ur.UserId = @UserId";

            conn.Open();
            var role = conn.Query<Role>(sql, new { user.UserId }).AsEnumerable().Select(_ => _.Name).ToArray();
            conn.Close();
            return role;
        }
    }
}
