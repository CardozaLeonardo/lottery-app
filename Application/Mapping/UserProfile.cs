using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Mapping
{
    class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, UserQuery>();
            CreateMap<User, GetUserQuery>();
            CreateMap<Role, CreateRoleCommand>();
            CreateMap<Role, RoleQuery>();
            CreateMap<Permission, PermissionQuery>();
        }
    }
}
