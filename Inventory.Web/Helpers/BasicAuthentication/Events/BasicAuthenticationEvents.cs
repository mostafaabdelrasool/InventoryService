// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicAuthenticationEvents.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The basic authentication events.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Web.Helpers.BasicAuthentication.Events
{
    #region Usings

    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;    
    using Microsoft.AspNetCore.Identity;    
    using System.Collections.Generic;
    using System.Security.Claims;
  using Inventory.Web.Models;
  #endregion

  /// <summary>
  /// The basic authentication events.
  /// </summary>
  public class BasicAuthenticationEvents
    {        
        public BasicAuthenticationEvents()
        {        
        }
        #region Public Properties

        /// <summary>
        /// Gets or sets a delegate assigned to this property will be invoked when the related method is called.
        /// </summary>
        public Func<ValidatePrincipalContext, Task<AuthenticateResult>> OnValidatePrincipal { get; set; } =
            context =>
            {
              // For now use a non IdentityUser (see models/user.cs)
                var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                
                
                var user = userManager.FindByNameAsync(context.UserName).Result;
                var roles = userManager.GetRolesAsync(user).Result;
                if (user != null || userManager.CheckPasswordAsync(user, context.Password).Result)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, context.UserName, context.Options.ClaimsIssuer)
                        
                    };

                    claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

                    var ticket = new AuthenticationTicket(
                        new ClaimsPrincipal(new ClaimsIdentity(claims, BasicAuthenticationDefaults.AuthenticationScheme)),
                        new Microsoft.AspNetCore.Authentication.AuthenticationProperties(),
                        BasicAuthenticationDefaults.AuthenticationScheme);

                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }

                return Task.FromResult(AuthenticateResult.Fail("Authentication failed."));
            };
   

    #endregion

    #region Implemented Interfaces

    #region IBasicAuthenticationEvents

    /// <summary>
    /// Called each time a request principal has been validated by the middleware. By implementing this method the
    /// application have alter or reject the principal which has arrived with the request.
    /// </summary>
    /// <param name="context">
    /// Contains information about the login session as well as the user name and provide password.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> representing the completed operation.
    /// </returns>
    public virtual Task<AuthenticateResult> ValidatePrincipal(ValidatePrincipalContext context) => this.OnValidatePrincipal(context);

        #endregion

        #endregion
    }
}
