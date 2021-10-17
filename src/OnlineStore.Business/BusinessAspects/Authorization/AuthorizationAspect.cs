using ATCommon.Aspect.Contracts.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using ATCommon.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using OnlineStore.Business.Contracts;
using OnlineStore.Business.DependencyResolvers.Autofac;

namespace OnlineStore.Business.BusinessAspects.Authorization
{
    public class AuthorizationAspectAttribute : InterceptionAttribute, IBeforeVoidInterception
    {
        public string Roles { get; set; }
        public void OnBefore(BeforeMethodArgs beforeMethodArgs)
        {
            var _httpContextAccessor = InstanceFactory.GetInstance<IHttpContextAccessor>();
            
            if (string.IsNullOrWhiteSpace(Roles))
            {
                throw new SecurityException("Invalid roles");
            }

            var roles = Roles.Split(",");

            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            bool isAuthorize = roles.Any(role => roleClaims.Contains(role));

            if (!isAuthorize)
            {
                throw new SecurityException("You are not authorized");
            }
        }
    }
}
