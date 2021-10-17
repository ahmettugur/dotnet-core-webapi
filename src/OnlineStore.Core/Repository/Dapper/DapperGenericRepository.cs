using Dapper;
using Dapper.Contrib.Extensions;
using OnlineStore.Core.Contracts.Entities;
using OnlineStore.Core.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OnlineStore.Core.Repository.Dapper
{
    public abstract class DapperGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected DapperGenericRepository()
        {
            
        }
        protected abstract IDbConnection Connection { get; }

        protected abstract string TableName { get; }
        
        public TEntity Add(TEntity entity)
        {
            using var conn = Connection;
            conn.Open();
            conn.Insert(entity);
            conn.Close();

            return entity;
        }

        public int Delete(TEntity entity)
        {
            using var conn = Connection;
            conn.Open();
            var result = conn.Delete(entity);
            conn.Close();
            return result ? 1 : 0;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity entity = null;

            var result = GenerateDynamicQuery.GetDynamicQuery(TableName, predicate);
            using var conn = Connection;
            conn.Open();
            entity = conn.Query<TEntity>(result.Sql, (object)result.Parameter).FirstOrDefault();
            conn.Close();
            return entity;
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            List<TEntity> entityList = null;

            if (predicate == null)
            {
                using var conn = Connection;
                conn.Open();
                entityList = conn.Query<TEntity>("SELECT * FROM " + TableName).ToList();
                conn.Close();
                return entityList;
            }

            using (var conn = Connection)
            {
                var result = GenerateDynamicQuery.GetDynamicQuery(TableName, predicate);
                conn.Open();
                entityList = conn.Query<TEntity>(result.Sql, (object)result.Parameter).ToList();
                conn.Close();
                return entityList;
            }
        }

        public TEntity Update(TEntity entity)
        {
            using var conn = Connection;
            conn.Open();
            conn.Update(entity);
            conn.Close();
            return entity;
        }
        
    }
}
