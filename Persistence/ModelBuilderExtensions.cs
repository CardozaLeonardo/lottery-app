using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                }
            );

            modelBuilder.Entity<Role>().HasData(
               new Role
               {
                   Id = 2,
                   Name = "Player"
               }
           );

            modelBuilder.Entity<User>().HasData(
            new User
            {   Id = 100,
                Name = "Jorge",
                LastName = "Morales",
                Password = "$2a$11$tBDwzGx2ogUlt826DdJ6ouWFwbeZdU.x.8tL1xGlIg7B/T4cNfQMG",
                Email = "jorgeProfe@gmail.com",
                Username = "jorginho777",
            }
           );

            modelBuilder.Entity<Player>().HasData(
            new Player
            {
                Id = 100,
                UserId = 100,
                Identification = "090-250989-1006U"
            }
           );

            modelBuilder.Entity<UserRole>().HasData(
            new UserRole
            {   UserId = 100,
                RoleId = 1
            }
           );


            modelBuilder.Entity<Permission>().HasData(
                  new Permission
                  {
                      PermissionId = 1,
                      Name = "Add User",
                      CodeName = "user.add"
                  }
            );

            modelBuilder.Entity<Permission>().HasData(
                 new Permission
                 {
                     PermissionId = 2,
                     Name = "Edit User",
                     CodeName = "user.edit"
                 }
            );

         
            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    PermissionId = 3,
                    Name = "Modify User",
                    CodeName = "user.modify"
                }
           );

            modelBuilder.Entity<Permission>().HasData(
                 new Permission
                 {
                     PermissionId = 4,
                     Name = "Delete User",
                     CodeName = "user.delete"
                 }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    PermissionId = 5,
                    Name = "List Users",
                    CodeName = "user.list"
                }
           );

            modelBuilder.Entity<Permission>().HasData(
                  new Permission
                  {
                      PermissionId = 6,
                      Name = "Add Role",
                      CodeName = "role.add"
                  }
            );

            modelBuilder.Entity<Permission>().HasData(
                 new Permission
                 {
                     PermissionId = 7,
                     Name = "Modify Role",
                     CodeName = "role.edit"
                 }
            );


            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    PermissionId = 8,
                    Name = "List Role",
                    CodeName = "role.list"
                }
           );

            modelBuilder.Entity<Permission>().HasData(
               new Permission
               {
                   PermissionId = 9,
                   Name = "List Permissions in Role",
                   CodeName = "role.permission.list"
               }
          );

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { RoleId = 1, PermissionID = 1},
                new RolePermission { RoleId = 1, PermissionID = 2 },
                new RolePermission { RoleId = 1, PermissionID = 3 },
                new RolePermission { RoleId = 1, PermissionID = 4},
                new RolePermission { RoleId = 1, PermissionID = 5 },
                new RolePermission { RoleId = 1, PermissionID = 6 },
                new RolePermission { RoleId = 1, PermissionID = 7 },
                new RolePermission { RoleId = 1, PermissionID = 8 },
                new RolePermission { RoleId = 1, PermissionID = 9}
            );

        }
    }
}
