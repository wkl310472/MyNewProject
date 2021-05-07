using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNewProject.Core;
using MyNewProject.Core.Models;

namespace MyNewProject.Persistence
{
    public class GenreRepository : IRepository<Genre>
    {
        private readonly GamestopDbContext context;
        public GenreRepository(GamestopDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Genre>> Get(Filter filter = null, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Genres.ToListAsync();
            }
            return await context.Genres
                .Include(g => g.Games)
                .ThenInclude(gg => gg.Game)
                .ToListAsync();
        }

        public async Task<Genre> Get(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Genres.FindAsync(id);
            }
            return await context.Genres
                .Include(g => g.Games)
                .ThenInclude(gg => gg.Game)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task Add(Genre genre)
        {
            await context.AddAsync(genre);
        }

        public void Remove(Genre genre)
        {
            context.Remove(genre);
        }
    }
}
