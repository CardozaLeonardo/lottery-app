using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Domain.Services;
using Domain.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly IPlayerManager _playerManager;
        private readonly IHashingService _hashingService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration , IObjectFactory factory, 
            IMapper mapper, IHashingService hashingService, IJwtService jwtService)
        {
            _configuration = configuration;
            _userManager = factory.Resolve<IUserManager>();
            _roleManager = factory.Resolve<IRoleManager>();
            _playerManager = factory.Resolve<IPlayerManager>();
            _hashingService = hashingService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(AuthCommand authCommand)
        {
            

            User user = _userManager.GetByUsernameOrEmail(authCommand.Username);

            if (user == null)
                return Unauthorized(new{ message = "Username or Password incorrect"});
            
            if (!_hashingService.VerifyPassword(authCommand.Password, user.Password))
                return Unauthorized(new { message = "Username or Password incorrect"});

            return Ok(new
            {
                Token = _jwtService.CreateToken(user.Username)
            }) ; 
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public IActionResult Me()
        {
            var username = HttpContext.User.Identity.Name;

            var userModel = _userManager.GetByUsernameWithRole(username);
            var userOutput = _mapper.Map<GetUserQuery>(userModel);
            var player = _playerManager.Get(userOutput.Id);

            userOutput.Roles = new List<RoleQuery>();

            foreach(var userRole in userModel.UserRoles)
            {
                userOutput.Roles.Add(_mapper.Map<RoleQuery>(userRole.Role));
            }

            if (player != null)
                userOutput.Player = _mapper.Map<PlayerQuery>(player); 

            return Ok(userOutput);
        }

        [Route("[action]")]
        [Authorize(Permissions.UserPermissions.TestPermission)]
        public IActionResult Test()
        {
            Debug.Print(HttpContext.User.Identity.Name);
            return Content("ok");
        }


    }
}
