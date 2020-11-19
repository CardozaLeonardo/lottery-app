using Application;
using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Domain.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController: BaseController<Role, CreateRoleCommand, RoleQuery>
    {
        private readonly IRoleManager _roleManager;

        public RoleController(IObjectFactory factory, IMapper mapper) : base()
        {
            _mapper = mapper;
            _roleManager = factory.Resolve<IRoleManager>();
        }

        [Route("[action]/{id}")]
        [Authorize(Permissions.RolePermissions.ListPermissions)]
        public async Task<ActionResult<IEnumerable<Permission>>> ListRolePermission(long roleId)
        {
            return await _roleManager.GetAllPermissionsInRole(roleId);
        }

        [Route("[action]")]
        [Authorize(Permissions.RolePermissions.ListPermissions)]
        public async Task<IActionResult> AddPermissionToRole(string permissionCodeName)
        {
            return Ok(new { message = "Hi"});
        }
    }
}
