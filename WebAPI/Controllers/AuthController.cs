using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Actions.UserActions;
using Domain.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager manager;
        public AuthController(IObjectFactory factory)
        {
            manager = factory.Resolve<IUserManager>();
        }

        [HttpPost]
        public IActionResult Authenticate(AuthCommand authCommand)
        {
            return Ok();
        }
    }
}
