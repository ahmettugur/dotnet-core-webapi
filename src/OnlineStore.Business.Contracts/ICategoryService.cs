using ATCommon.Utilities.Response;
using OnlineStore.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnlineStore.Business.Contracts
{
    public interface ICategoryService
    {
        IResult<List<Category>> GetAll(Expression<Func<Category, bool>> predicate = null);
        IResult<Category> Get(Expression<Func<Category, bool>> predicate);
        IResult<Category> Add(Category entity);
        IResult<Category> Update(Category entity);
        IResult<int> Delete(Category entity);
    }
}
