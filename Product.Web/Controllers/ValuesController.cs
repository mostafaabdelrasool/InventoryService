using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Product.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(string jwt)
        {
           // ValidateAndDecode(jwt);
            return new string[] { "value1", "value2" };
        }
        private static JwtSecurityToken ValidateAndDecode(string jwt)
        {
            var validationParameters = new TokenValidationParameters
            {
                // Clock skew compensates for server time drift.
                // We recommend 5 minutes or less:
                // Specify the key used to sign the token:
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("I-Am-A-Key6zh8fOZCjZG1S*1B7msLt5v33ah6yMLep6JcrCL4EpDT3qS%14^2")),
                //ValidAudience = "http://localhost:44358/",
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                //ValidIssuer = "http://localhost:44358/"
                ValidateAudience = false,
                ValidateIssuer=false
            };

            try
            {
                var claimsPrincipal = new JwtSecurityTokenHandler()
                    .ValidateToken(jwt, validationParameters, out var rawValidatedToken);

                return (JwtSecurityToken)rawValidatedToken;
                // Or, you can return the ClaimsPrincipal
                // (which has the JWT properties automatically mapped to .NET claims)
            }
            catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                // TODO: Log it or display an error.
                throw new Exception($"Token failed validation: {stvex.Message}");
            }
            catch (ArgumentException argex)
            {
                // The token was not well-formed or was invalid for some other reason.
                // TODO: Log it or display an error.
                throw new Exception($"Token was invalid: {argex.Message}");
            }
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
