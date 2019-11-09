using OnlineStore.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Entity.Concrete;
using System.Linq.Expressions;
using OnlineStore.Data.Contracts;
using ATCommon.Utilities.Response;

namespace OnlineStore.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRespository _userRespository;

        public UserService(IUserRespository userRespository)
        {
            _userRespository = userRespository;
        }

        public IResult<User> Add(User entity)
        {
            var result = _userRespository.Add(entity);
            return new Result<User>(201,result);
        }

        public IResult<int> Delete(User entity)
        {
            var result = _userRespository.Delete(entity);
            return new Result<int>(200,result);
        }

        public IResult<User> Get(Expression<Func<User, bool>> predicate)
        {
            var result = _userRespository.Get(predicate);
            return new Result<User>(200,result);
        }

        public IResult<List<User>> GetAll(Expression<Func<User, bool>> predicate = null)
        {
            var result = _userRespository.GetAll(predicate);
            return new Result<List<User>>(200,result);
        }

        public IResult<string[]> GetUserRoles(User user)
        {
            var result = _userRespository.GetUserRoles(user);
            return new Result<string[]>(200,result);
        }

        public IResult<User> Update(User entity)
        {
            var result = _userRespository.Update(entity);
            return new Result<User>(200,result);
        }
    }
}
