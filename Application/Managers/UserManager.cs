using Domain.Managers;
using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Managers
{
    public class UserManager: BaseManager<User>, IUserManager
    {
        public UserManager(LotteryAppContext context): base(context)
        {
            _context = context;
            _dbSet = context.Users;
        }

        public string Login()
        {

        }

    }
}
