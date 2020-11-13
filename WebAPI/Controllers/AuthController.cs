using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserManager _userManager;

        public AuthController(IConfiguration configuration , IObjectFactory factory, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = factory.Resolve<IUserManager>();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(AuthCommand model)
        {
            
            // Leemos el secret_key desde nuestro appseting
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}

using Application;
using Application.Actions.UserActions;
using Domain.Managers;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager manager;
        private readonly IHashingService _hashingService;
        public AuthController(IObjectFactory factory, IHashingService hashingService)
        {
            manager = factory.Resolve<IUserManager>();
            _hashingService = hashingService;
        }

        [HttpPost]
        public IActionResult Authenticate(AuthCommand authCommand)
        {
            var user = manager.GetByUsernameOrEmail(authCommand.Username);

            if(user == null)
            {
                return Unauthorized(new
                {
                    message = "Username or Password incorrect"
                });
            }

            if(!_hashingService.VerifyPassword(authCommand.Password, user.Password))
            {
                return Unauthorized(new
                {
                    message = "Username or Password incorrect"
                });
            }


            return Ok(
            new
            {
                message = "Welcome " + user.Username + "!"
            });

            
            
            //return Ok();
        }
    }
}
