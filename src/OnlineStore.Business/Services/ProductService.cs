using OnlineStore.Business.Contracts;
using OnlineStore.Data.Contracts;
using ATCommon.Utilities.Response;
using OnlineStore.Entity.ComplexType;
using OnlineStore.Entity.Concrete;
using OnlineStore.MQ.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnlineStore.Business.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private RabbitMQEntityPost<Product> rabbitMQ;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            rabbitMQ = new RabbitMQEntityPost<Product>("ProductQueue");
            
        }

        public IResult<Product> Add(Product entity)
        {
            var result = _productRepository.Add(entity);
            return new Result<Product>(201,result);
        }

        public IResult<int> Delete(Product entity)
        {
            var result = _productRepository.Delete(entity);
            return new Result<int>(200,result);
        }

        public IResult<Product> Get(Expression<Func<Product, bool>> predicate)
        {
            var product = _productRepository.Get(predicate);
            return new Result<Product>(200,product);
        }


        public IResult<List<Product>> GetAll(Expression<Func<Product, bool>> predicate = null)
        {
            var result = _productRepository.GetAll(predicate);
            return new Result<List<Product>>(200,result);
        }

        public IResult<List<ProductWithCategory>> GetAllProductWithCategory()
        {
            var result = _productRepository.GetAllProductWithCategory();
            return new Result<List<ProductWithCategory>>(200,result);
        }

        public IResult<Product> Update(Product entity)
        {
            var product =  _productRepository.Update(entity);
            rabbitMQ.Post(product);
            return new Result<Product>(200,product);
        }
    }
}
