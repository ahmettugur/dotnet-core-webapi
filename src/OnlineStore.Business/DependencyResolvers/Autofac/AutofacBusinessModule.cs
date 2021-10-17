using Autofac;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Business.Contracts;
using OnlineStore.Business.Services;
using OnlineStore.Data.Contracts;
using OnlineStore.Data.Dapper;
using OnlineStore.Data.EntityFramework;
using OnlineStore.Data.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using ATCommon.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Http;

namespace OnlineStore.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<LogService>().As<ILogService>();


            builder.RegisterType<EFProductRepository>().As<IProductRepository>();
            builder.RegisterType<EFCategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<EFUserRepository>().As<IUserRespository>();
            builder.RegisterType<EFLogRepository>().As<ILogRepository>();
            builder.RegisterType<OnlineStoreContext>().As<DbContext>();
            
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        }
    }
}
