using Domain.Entities;
using Domain.Managers;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Application.Managers
{
    public class RoleManager: BaseManager<Role>, IRoleManager
    {
        public RoleManager(LotteryAppContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Roles;
        }

        public async Task<List<Permission>> GetAllPermissionsInRole(long roleId)
        {
            List<Permission> permissions = new List<Permission>();
            Role role = await _dbSet.Include(r => r.RolePermissions).ThenInclude(rp => rp.Permission).SingleOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
                return null;

            foreach(RolePermission rolePermission in role.RolePermissions)
            {
                permissions.Add(rolePermission.Permission);
            }

            return permissions;
        }

        public async Task<bool> CheckPermissionInRole(long roleId , string permissionCodeName)
        {
            Role role = await _dbSet.Include(r => r.RolePermissions).ThenInclude(rp => rp.Permission).SingleOrDefaultAsync(r => r.Id == roleId);

            if (role.RolePermissions == null)
                return false;


            foreach (RolePermission rolePermission in role.RolePermissions)
            {
                if (rolePermission.Permission.CodeName == permissionCodeName)
                    return true;
            }

            return false;
        }

        public async Task<Permission> AddPermissionToRole(long roleId  ,string permissionCodeName)
        {
            Permission permission = await _context.Permission.SingleOrDefaultAsync(p => p.CodeName == permissionCodeName);
            Role role = await _dbSet.SingleOrDefaultAsync(r => r.Id == roleId);

            if(permission.PermissionId != 0 && role.Id == 0)
            {
                RolePermission rolePermission = new RolePermission()
                {
                    RoleId = roleId,
                    PermissionID = permission.PermissionId
                };

                _context.RolePermission.Add(rolePermission);
                await _context.SaveChangesAsync();
                return permission;
            }

            //this may need better handling
            return null;
        }

        public async Task<bool> AddUserToRole(long userId, long roleId)
        {
            User user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            Role role = await _dbSet.SingleOrDefaultAsync(r => r.Id == roleId);

            if (user.Id != 0 && role.Id != 0)
            {
                UserRole userRole = new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                };

                _context.UserRole.Add(userRole);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    } 
}
