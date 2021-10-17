﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ATCommon.Utilities.Extensions;
using ATCommon.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineStore.Entity.Concrete;

namespace ATCommon.Utilities.Security.Jwt
{
    public class JwtHelper:ITokenHelper
    {
        private IConfiguration Configuration { get; }
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = JsonConvert.DeserializeObject<TokenOptions>(Configuration.GetSection("TokenOptions").Value);
            
        }
        public AccessToken CreateToken(User user, string[] roles)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roles);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, 
            SigningCredentials signingCredentials, string[] roles)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audience,
                expires:_accessTokenExpiration,
                notBefore:DateTime.Now,
                claims: SetClaims(user,roles),
                signingCredentials:signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, string[] roles)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.UserId.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FullName}");
            claims.AddRoles(roles);
            
            return claims;
        }
    }
}
