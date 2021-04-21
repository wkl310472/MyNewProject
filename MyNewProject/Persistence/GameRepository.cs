using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNewProject.Models;

namespace MyNewProject.Persistence
{
    public class GameRepository : IGameRepository
    {
        private readonly GamestopDbContext context;
        public GameRepository(GamestopDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Game>> Get(bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Games.ToListAsync();
            }
            return await context.Games
                .Include(g => g.Genres)
                .ThenInclude(gg => gg.Genre)
                .Include(g => g.Platforms)
                .ThenInclude(gp => gp.Platform)
                .ToListAsync();
        }

        public async Task<Game> Get(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Games.FindAsync(id);
            }
            return await context.Games
                .Include(g => g.Genres)
                .ThenInclude(gg => gg.Genre)
                .Include(g => g.Platforms)
                .ThenInclude(gp => gp.Platform)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public void Add(Game game)
        {
            this.context.Add(game);
        }

        public void Remove(Game game)
        {
            this.context.Remove(game);
        }
    }
}
