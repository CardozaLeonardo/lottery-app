using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class LotteryAppContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Raffle> Raffles { get; set; }
        public DbSet<PlayerRaffle> PlayerRaffle { get; set; }
        public DbSet<Winner> Winner { get; set; }

        public LotteryAppContext(DbContextOptions<LotteryAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);


            modelBuilder.Entity<Permission>().HasIndex(p => p.CodeName).IsUnique();

            modelBuilder.Entity<RolePermission>()
              .HasKey(rp => new { rp.RoleId, rp.PermissionID });

            modelBuilder.Entity<RolePermission>()
               .HasOne(rp => rp.Role)
               .WithMany(r => r.RolePermissions)
               .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
             .HasOne(rp => rp.Permission)
             .WithMany(r => r.RolePermissions)
             .HasForeignKey(rp => rp.PermissionID);

            modelBuilder.Entity<PlayerRaffle>()
            .HasOne(pr => pr.Raffle)
            .WithMany(r => r.PlayerRaffles)
            .HasForeignKey(rp => rp.RaffleId);

            modelBuilder.Entity<PlayerRaffle>()
           .HasOne(pr => pr.Player)
           .WithMany(p => p.RaffleTickets)
           .HasForeignKey(rp => rp.PlayerId);

            modelBuilder.Seed();
        }
    }
}
