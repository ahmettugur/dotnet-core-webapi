﻿using ATCommon.Aspect.Logging;
using ATCommon.Logging.Log4net.Loggers;
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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public IResult<Category> Add(Category entity)
        {
            return new Result<Category>(201,_categoryRepository.Add(entity));
        }


        public IResult<int> Delete(Category entity)
        {
            return new Result<int>(200,_categoryRepository.Delete(entity));
        }

        public IResult<Category> Get(Expression<Func<Category, bool>> predicate)
        {
            return new Result<Category>(200,_categoryRepository.Get(predicate));
        }

        [BeforeLogAspect(typeof(JsonFileLogger))]
        public IResult<List<Category>> GetAll(Expression<Func<Category, bool>> predicate = null)
        {
            return new Result<List<Category>>(200, _categoryRepository.GetAll(predicate).OrderByDescending(_ => _.Id).ToList());
        }

        public IResult<Category> Update(Category entity)
        {
            return new Result<Category>(200,_categoryRepository.Update(entity));
        }
    }
}
