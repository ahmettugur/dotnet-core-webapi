using ATCommon.Utilities.Response;
using OnlineStore.Business.Contracts;
using OnlineStore.Data.Contracts;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineStore.Business.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository LogRepository)
        {
            _logRepository = LogRepository;
        }


        public IResult<Log> Add(Log entity)
        {
            return new Result<Log>(201,_logRepository.Add(entity));
        }


        public IResult<int> Delete(Log entity)
        {
            return new Result<int>(200,_logRepository.Delete(entity));
        }

        public IResult<Log> Get(Expression<Func<Log, bool>> predicate)
        {
            return new Result<Log>(200,_logRepository.Get(predicate));
        }

        public IResult<List<Log>> GetAll(Expression<Func<Log, bool>> predicate = null)
        {
            var model = _logRepository.GetAll(predicate).OrderByDescending(_ => _.Id).ToList();
            return new Result<List<Log>>(200, model);
        }

        public IResult<Log> Update(Log entity)
        {
            return new Result<Log>(200,_logRepository.Update(entity));
        }
    }
}
