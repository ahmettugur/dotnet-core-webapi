using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using OnlineStore.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OnlineStore.Business.Contracts;
using OnlineStore.Entity.Concrete;
using Newtonsoft.Json;

namespace OnlineStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IUserService _userService;
        public TokenController(IConfiguration configuration, IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult Post([FromBody]LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                //This method returns user id from username and password.
                var user = GetUserIdFromCredentials(loginViewModel);
                if (user == null)
                {
                    return NotFound("Invaliid Username and password");
                }
                
                var result = _userService.CreateAccessToken(user);
                if (result.StatusCode == 200)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.ErrorMessage);
                
            }

            return BadRequest();
        }

        private User GetUserIdFromCredentials(LoginViewModel loginViewModel)
        {
            var user = _userService.Get(_ => _.Email == loginViewModel.Email && _.Password == loginViewModel.Password).Data;
            return user;
        }
    }
}