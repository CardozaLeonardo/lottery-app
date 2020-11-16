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
        private readonly IHashingService _hashingService;

        public UserController(IObjectFactory factory, IMapper mapper, IHashingService hashingService) : base()
        {
            _userManager = factory.Resolve<IUserManager>();
            _mapper = mapper;
            _hashingService = hashingService;
        }

        [HttpPost]
        public override async Task<ActionResult<User>> Post(CreateUserCommand item)
        {
            var user = _userManager.GetByEmail(item.Email);

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

            return Ok("Created");

        }
    }
}
