using Domain.Entities;
using Domain.Managers;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Managers
{
    public class PlayerManager: BaseManager<Player>, IPlayerManager
    {
        public PlayerManager(LotteryAppContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Players;
        
            
        }
    }
}
