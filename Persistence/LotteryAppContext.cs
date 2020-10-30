using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class LotteryAppContext: DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public LotteryAppContext(DbContextOptions<LotteryAppContext> options): base(options)
        {

        }
    }
}
