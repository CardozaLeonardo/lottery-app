
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
