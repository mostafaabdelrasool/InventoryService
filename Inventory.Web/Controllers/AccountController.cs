using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _config = config;

        }
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName,
                          login.Password, login.RememberMe, lockoutOnFailure: true);
            if (!result.Succeeded)
                //Authenticate User, Check if it’s a registered user in Database
                return null;
            //Authentication successful, Issue Token with user credentials
            //Provide the security key which was given in the JWToken configuration in Startup.cs
            var key = Encoding.UTF8.GetBytes(_config.GetSection("AppConfiguration:Key").Value);
            //Generate Token for user 
            var JWToken = new JwtSecurityToken(
                issuer: _config.GetSection("AppConfiguration:SiteUrl").Value,
                audience: _config.GetSection("AppConfiguration:SiteUrl").Value,
                //claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                //Using HS256 Algorithm to encrypt Token
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            return Ok(token);
        }
    }
}