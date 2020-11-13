using Domain.Managers;
using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Managers
{
    public class UserManager: BaseManager<User>, IUserManager
    {
        public UserManager(LotteryAppContext context): base(context)
        {
            _context = context;
            _dbSet = context.Users;
        }

        public User GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(p => p.Email == email);
        }

        public User GetByUsername(string username)
        {
            return _dbSet.FirstOrDefault(p => p.Username == username);
        }

        public virtual User GetByUsernameOrEmail(string term)
        {
            return _dbSet.FirstOrDefault(p => p.Username == term || p.Email == term);
        }

        public string Login()
        {

        }

    }
}
