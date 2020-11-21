using System;
using System.Collections.Generic;
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
        private readonly IHashingService _hashingService;
        private readonly IJwtService _jwtService;

        public AuthController(IConfiguration configuration , IObjectFactory factory, 
            IMapper mapper, IHashingService hashingService, IJwtService jwtService)
        {
            _configuration = configuration;
            _userManager = factory.Resolve<IUserManager>();
            _hashingService = hashingService;
            _jwtService = jwtService;
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
        
        [Route("[action]")]
        [Authorize(Permissions.UserPermissions.TestPermission)]
        public IActionResult Test()
        {
            return Content("ok");
        }

    }
}
