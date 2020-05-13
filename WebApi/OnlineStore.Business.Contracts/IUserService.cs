using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using ATCommon.Utilities.Response;
using System.Linq.Expressions;
using ATCommon.Utilities.Security.Jwt;

namespace OnlineStore.Business.Contracts
{
    public interface IUserService
    {
        IResult<List<User>> GetAll(Expression<Func<User,bool>> predicate = null);
        IResult<User> Get(Expression<Func<User,bool>> predicate);
        IResult<User> Add(User entity);
        IResult<User> Update(User entity);
        IResult<int> Delete(User entity);
        IResult<string[]> GetUserRoles(User user);
        IResult<AccessToken> CreateAccessToken(User user);

    }
}
