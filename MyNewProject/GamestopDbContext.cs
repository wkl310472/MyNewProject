using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Models;

namespace MyNewProject
{
    public class GamestopDbContext : DbContext
    {

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }

        public GamestopDbContext(DbContextOptions<GamestopDbContext> options):base(options)
        {

        }
    }
}
