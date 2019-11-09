using OnlineStore.Entity.ComplexType;
using OnlineStore.Entity.Concrete;
using ATCommon.Utilities.Response;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnlineStore.Business.Contracts
{
    public interface IProductService
    {
        IResult<List<Product>> GetAll(Expression<Func<Product, bool>> predicate = null);
        IResult<List<ProductWithCategory>> GetAllProductWithCategory();
        IResult<Product> Get(Expression<Func<Product, bool>> predicate);
        IResult<Product> Add(Product entity);
        IResult<Product> Update(Product entity);
        IResult<int> Delete(Product entity);
    }
}
