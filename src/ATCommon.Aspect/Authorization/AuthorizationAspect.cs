using ATCommon.Aspect.Contracts.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;

namespace ATCommon.Aspect.Authorization
{
    public class AuthorizationAspectAttribute : InterceptionAttribute, IBeforeVoidInterception
    {
        public string Roles { get; set; }
        public void OnBefore(BeforeMethodArgs beforeMethodArgs)
        {
            if (string.IsNullOrWhiteSpace(Roles))
            {
                throw new SecurityException("Invalid roles");
            }

            var roles = Roles.Split(",");
            bool isAuthorize = roles.Any(role => Thread.CurrentPrincipal.IsInRole(role));

            if (!isAuthorize)
            {
                throw new SecurityException("You are not authorized");
            }
        }
    }
}
