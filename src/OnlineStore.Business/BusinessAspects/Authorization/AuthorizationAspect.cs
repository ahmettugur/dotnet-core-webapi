using ATCommon.Aspect.Contracts.Interception;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading;
using ATCommon.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using OnlineStore.Business.Contracts;
using OnlineStore.Business.DependencyResolvers.Autofac;

namespace OnlineStore.Business.BusinessAspects.Authorization
{
    public class AuthorizationAspect : InterceptionAttribute, IBeforeVoidInterception
    {
        public string Roles { get; set; }
        public void OnBefore(BeforeMethodArgs beforeMethodArgs)
        {
            IHttpContextAccessor _httpContextAccessor = InstanceFactory.GetInstance<IHttpContextAccessor>();;
            
            if (string.IsNullOrWhiteSpace(Roles))
            {
                throw new Exception("Invalid roles");
            }

            var roles = Roles.Split(",");
            bool isAuthorize = false;

            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in roles)
            {
                if (roleClaims.Contains(role))
                {
                    isAuthorize = true;
                    break;
                }
            }

            if (isAuthorize == false)
            {
                throw new SecurityException("You are not authorized");
            }
        }
    }
}
