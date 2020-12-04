using Domain.Managers;
using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public virtual User GetByUsernameOrEmailWithRole(string term)
        {
            return _dbSet.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(p => p.Username == term || p.Email == term);
        }

        public User GetByUsernameWithRole(string username)
        {
            return _dbSet.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(p => p.Username == username);
        }

        public virtual User GetWithRole(long id)
        {
            return _dbSet.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(p => p.Id == id);
        }
    }
}
