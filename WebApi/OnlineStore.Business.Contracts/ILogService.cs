using ATCommon.Utilities.Response;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnlineStore.Business.Contracts
{
    public interface ILogService
    {
        IResult<List<Log>> GetAll(Expression<Func<Log, bool>> predicate = null);
        IResult<Log> Get(Expression<Func<Log, bool>> predicate);
        IResult<Log> Add(Log entity);
        IResult<Log> Update(Log entity);
        IResult<int> Delete(Log entity);
    }
}
