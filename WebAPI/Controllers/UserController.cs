using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User, CreateUserCommand, UserQuery>
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly IHashingService _hashingService;

        public UserController(IObjectFactory factory, IMapper mapper, IHashingService hashingService) : base()
        {
            _userManager = factory.Resolve<IUserManager>();
            _roleManager = factory.Resolve<IRoleManager>();
            _manager = factory.Resolve<IUserManager>();
            _mapper = mapper;
            _hashingService = hashingService;
        }

        [HttpPost]
        public override async Task<ActionResult<User>> Post(CreateUserCommand item)
        {
            var user = _userManager.GetByEmail(item.Email);
            var role = await _roleManager.Get(item.RoleId);

            if(role == null)
            {
                return Conflict(new { 
                    Message = "This role doesn't exist",
                    Field =  "roleId"
                });
            }


            if(user != null)
            {
                return Conflict(new
                {
                    message = "This email is already registered",
                    field = "email"
                });
            }

            user = _userManager.GetByUsername(item.Username);


            if (user != null)
            {
                return Conflict(new
                {
                    message = "This username is already registered",
                    field = "username"
                });
            }


            var userModel = _mapper.Map<User>(item);

            var hashPassword = _hashingService.HashPassword(userModel.Password);
            userModel.Password = hashPassword;
            await _userManager.Add(userModel);
            await _roleManager.AddUserToRole(userModel.Id, item.RoleId);

            var userOutput = _mapper.Map<UserQuery>(userModel);

            return Created("/user", userOutput);

        }

        /*[Route("me")]
        [HttpGet]
        public async Task<ActionResult<>>*/


    }
}
