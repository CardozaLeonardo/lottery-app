using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using Domain.Managers;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IPlayerManager _playerManager;

        public UserController(IObjectFactory factory, IMapper mapper, IHashingService hashingService) : base()
        {
            _userManager = factory.Resolve<IUserManager>();
            _playerManager = factory.Resolve<IPlayerManager>();
            _roleManager = factory.Resolve<IRoleManager>();
            _mapper = mapper;
            _hashingService = hashingService;
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public virtual async Task<ActionResult<IEnumerable<UserQuery>>> List(int? pageNumber, int pageSize = _defaultPageSize)
        {
            try
            {
                IQueryable<User> query = _userManager.List();
                var itemCount = query.Count();
                int? pages = GetPages(itemCount, pageNumber, pageSize);

                if (pageNumber.HasValue)
                    query = Paginate(query, pageNumber.Value);
                else
                {
                    Paginate(query, 1);
                }

                var items = query.ToList();

                var userItems = _mapper.Map<IEnumerable<UserQuery>>(items);

                return Ok(new
                {
                    Total = itemCount,
                    Pages = pages,
                    Data = userItems
                });

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                LogManager.Current.Log.Error(e);
                return StatusCode(500, "Internal Server Error " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                var item = _userManager.GetWithRole(Convert.ToInt64(id));

                if (item == null)
                {
                    return NotFound();
                }

                
                return item;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                LogManager.Current.Log.Error(e);
                return StatusCode(500, "Internal Server Error " + e.Message);
            }
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

            await _playerManager.Add(new Player
            {
                UserId = userModel.Id
            });


            return Created("/user", userOutput);

        }

        /*[Route("me")]
        [HttpGet]
        public async Task<ActionResult<>>*/


    }
}
