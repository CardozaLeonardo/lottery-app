using Domain.Managers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Security.Claims;

namespace Application.Security
{
    class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;

        public PermissionAuthorizationHandler(IObjectFactory factory) : base()
        {
            _userManager = factory.Resolve<IUserManager>();
            _roleManager = factory.Resolve<IRoleManager>();
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }

            // Get all the roles the user belongs to and check if any of the roles has the permission required
            // for the authorization to succeed.
            var user = await _userManager.Get(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
                                                        x.Value == requirement.Permission &&
                                                        x.Issuer == "LOCAL AUTHORITY")
                                            .Select(x => x.Value);

                if (permissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }

}
}
