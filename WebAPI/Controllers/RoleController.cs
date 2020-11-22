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

        [Route("list-role-permissions/{roleId}")]
        [Authorize(Permissions.RolePermissions.ListPermissions)]
        public async Task<ActionResult<IEnumerable<Permission>>> ListRolePermission(long roleId)
        {
            List<PermissionQuery> result = _mapper.Map<List<PermissionQuery>>(await _roleManager.GetAllPermissionsInRole(roleId));
            return Ok(result);
        }

        [Route("[action]")]
        public async Task<IActionResult> AddPermissionToRole(string permissionCodeName)
        {
            return Ok(new { message = "Hi"});
        }

        [Route("add-user")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleCommand addUserToRoleCommand)
        {
            bool result = await _roleManager.AddUserToRole(addUserToRoleCommand.UserId, addUserToRoleCommand.RoleId);

            if (result)
                return StatusCode(201);  
            return StatusCode(400);
        }

    }
}
