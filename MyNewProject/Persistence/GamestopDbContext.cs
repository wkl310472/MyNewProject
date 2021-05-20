using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core.Models;

namespace MyNewProject.Persistence
{
    public class GamestopDbContext : DbContext
    {

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLevel> UserLevels { get; set; }

        public GamestopDbContext(DbContextOptions<GamestopDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameGenre>().HasKey(gg => new { gg.GameId, gg.GenreId });
            modelBuilder.Entity<GamePlatform>().HasKey(gp => new { gp.GameId, gp.PlatformId });

            modelBuilder.Entity<UserLevel>().HasMany(l => l.Users).WithOne(u => u.UserLevel).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserLevel>().HasIndex(l => l.Level).IsUnique();
            modelBuilder.Entity<User>().HasOne(u => u.UserLevel).WithMany(l => l.Users).HasForeignKey(u => u.Level).HasPrincipalKey(l => l.Level);
            modelBuilder.Entity<User>().Property(u => u.Money).HasDefaultValue(0.0);
            modelBuilder.Entity<User>().Property(u => u.Level).HasDefaultValue(4);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
