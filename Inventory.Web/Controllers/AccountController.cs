using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _config = config;
            _userManager = userManager;
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
            string token = TokenGeneration(login.UserName);
            return Ok(token);
        }
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            var user = new IdentityUser { UserName = login.UserName, Email = login.Email};
            var result = await _userManager.CreateAsync(user, login.Password);
            if (!result.Succeeded)
                //Authenticate User, Check if it’s a registered user in Database
                return BadRequest(result.Errors);
            string token = TokenGeneration(login.Email);
            return Ok(token);
        }
        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public void xx()
        
        {
           
        }
        private string TokenGeneration(string id)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,id)
            };

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
                                    SecurityAlgorithms.HmacSha256Signature),
                claims:claims
            );
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            return token;
        }
    }
}