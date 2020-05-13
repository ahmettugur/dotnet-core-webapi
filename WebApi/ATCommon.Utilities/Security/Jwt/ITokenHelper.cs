using System;
using System.Collections.Generic;
using System.Text;
using OnlineStore.Entity.Concrete;

namespace ATCommon.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, string[] roles);
    }
}
