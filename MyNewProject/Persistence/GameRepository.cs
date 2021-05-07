using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNewProject.Core;
using MyNewProject.Core.Models;

namespace MyNewProject.Persistence
{
    public class GameRepository : IRepository<Game>
    {
        private readonly GamestopDbContext context;
        public GameRepository(GamestopDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Game>> Get(Filter filter,bool includeRelated = true)
        {
            List<Game> games;
            if (!includeRelated)
            {
                games = await context.Games.ToListAsync();
            }
            else
            {
                games = await context.Games
                .Include(g => g.Genres)
                .ThenInclude(gg => gg.Genre)
                .Include(g => g.Platforms)
                .ThenInclude(gp => gp.Platform)
                .Include(g => g.Photos)
                .ToListAsync();
            }

            if (filter != null)
            {
                games = games.Where(g => applyFilter(g,filter)).ToList();
            }

            return games; 
        }

        private bool applyFilter(Game game, Filter filter)
        {
            if (filter.GenreId.Count() > 0)
            {
                foreach (var genreId in filter.GenreId)
                {
                    if (!game.Genres.Select(g => g.GenreId).Contains(genreId))
                    {
                        return false;
                    }
                }
            }

            if (filter.PlatformId.Count() > 0)
            {
                foreach (var platformId in filter.PlatformId)
                {
                    if (!game.Platforms.Select(g => g.PlatformId).Contains(platformId))
                    {
                        return false;
                    }
                }
            }


            return true;
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
                .Include(g => g.Photos)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task Add(Game game)
        {
            await context.AddAsync(game);
        }

        public void Remove(Game game)
        {
            context.Remove(game);
        }
    }
}
