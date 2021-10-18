using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Channels;
using Moq;
using OnlineStore.Business.Contracts;
using OnlineStore.Business.Services;
using OnlineStore.Data.Contracts;
using OnlineStore.Entity.Concrete;
using Xunit;

namespace OnlineStore.Business.Tests
{
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _mock;
        private readonly ICategoryService _categoryService;
        private readonly List<Category> _categories;

        public CategoryServiceTest()
        {
            _mock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_mock.Object);
            _categories = new List<Category>
            {
                new Category() { Id = 1, Name = "Laptop", Description = "MacBook" }
            };
        }
        
        [Theory]
        [InlineData(1)]
        public void GetCategory_InValidId_ReturnIResult(int categoryId)
        {
            Category category = null;
            _mock.Setup(_ => _.Get(_ => _.Id == categoryId)).Returns(category);

            var result = _categoryService.Get(_ => _.Id == categoryId);
            
            Assert.IsNotType<Category>(result.Data);
            Assert.Null(result.Data);
            Assert.Equal(404,result.StatusCode);
            Assert.Equal("Category cannot found",result.ErrorMessage);
        }

        [Theory]
        [InlineData(1)]
        public void GetCategory_ValidId_ReturnIResult(int categoryId)
        {
            Category category = _categories.First(x => x.Id == categoryId);
            _mock.Setup(_ => _.Get(_ => _.Id == categoryId)).Returns(category);

            var result = _categoryService.Get(_ => _.Id == categoryId);
            
            Assert.IsType<Category>(result.Data);
            Assert.Equal(200,result.StatusCode);
            Assert.Equal(_categories.First().Id, result.Data.Id);
        }
        
        [Fact]
       public void GetCategory_ExecuteMethod_ReturnIResult()
        {
            _mock.Setup(_ => _.GetAll(null)).Returns(_categories);

            var result = _categoryService.GetAll();
            
            Assert.IsType<List<Category>>(result.Data);
            Assert.NotNull(result.Data);
            Assert.Equal(200,result.StatusCode);
        }
        [Fact]
        public void GetCategory_ExecuteMethod_ReturnDataNull()
        {
            List<Category> categories = null;
            _mock.Setup(_ => _.GetAll(_ => _.Name == "Demo")).Returns(categories);

            var result = _categoryService.GetAll(_ => _.Name == "Demo");

            Assert.IsNotType<List<Category>>(result.Data);
            Assert.Null(result.Data);
        }

        [Fact]
        public void AddCategory_ExecuteMethod_ReturnAddedCategory()
        {
            var category = _categories.First();
            _mock.Setup(_=>_.Add(category)).Returns(category);

            var result = _categoryService.Add(category);

            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<Category>(result.Data);
        }

        [Fact]
        public void UpdateCategory_ExecuteMethod_ReturnUpdatedCategory()
        {
            var category = _categories.First();
            category.Name = "Keyboard";
            _mock.Setup(_ => _.Update(category)).Returns(category);

            var result = _categoryService.Update(category);

            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<Category>(result.Data);
            Assert.Equal("Keyboard", result.Data.Name);
        }

        [Fact]
        public void DeleteCategory_ExecuteMethod_ReturnDeletedCategoryCount()
        {
            var category = _categories.First();
            _mock.Setup(_ => _.Delete(category)).Returns(1);

            var result = _categoryService.Delete(category);
            Assert.Equal(1, result.Data);
        }
    }
}