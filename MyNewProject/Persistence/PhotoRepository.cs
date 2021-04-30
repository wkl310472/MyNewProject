using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNewProject.Core;
using MyNewProject.Core.Models;

namespace MyNewProject.Persistence
{
    public class PhotoRepository:IPhotoRepository
    {
        private readonly GamestopDbContext context;

        public PhotoRepository(GamestopDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int GameId)
        {
            return await context.Photos.Where(p => p.GameId == GameId).ToListAsync();
        }
    }
}
