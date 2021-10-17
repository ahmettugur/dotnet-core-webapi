using Microsoft.IdentityModel.Tokens;

namespace ATCommon.Utilities.Security.Encyption
{
    public class SigningCredentialsHelper
    {
        protected SigningCredentialsHelper()
        {
            
        }
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}