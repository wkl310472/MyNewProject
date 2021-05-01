using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core;
using MyNewProject.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MyNewProject.Persistence
{
    public class PlatformRepository : IRepository<Platform>
    {
        private readonly GamestopDbContext context;
        public PlatformRepository(GamestopDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Platform>> Get(bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Platforms.ToListAsync();
            }
            return await context.Platforms
                .Include(p => p.Games)
                .ThenInclude(gp => gp.Game)
                .ToListAsync();
        }

        public async Task<Platform> Get(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await context.Platforms.FindAsync(id);
            }
            return await context.Platforms
                .Include(p => p.Games)
                .ThenInclude(gp => gp.Game)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(Platform platform)
        {
            await context.AddAsync(platform);
        }

        public void Remove(Platform platform)
        {
            context.Remove(platform);
        }
    }
}
