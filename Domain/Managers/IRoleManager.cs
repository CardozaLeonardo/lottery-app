using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Managers
{
    public interface IRoleManager: IBaseManager<Role>
    {
        Task<List<Permission>> GetAllPermissionsInRole(long RoleId);
        Task<bool> CheckPermissionInRole(long roleId, string permissionCodeName);
        Task<Permission> AddPermissionToRole(long roleId, string permissionCodeName);
        Task<bool> AddUserToRole(long userId, long roleId);


    }
}
