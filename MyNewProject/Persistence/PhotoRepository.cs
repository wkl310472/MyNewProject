using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyNewProject.Core;
using MyNewProject.Core.Models;

namespace MyNewProject.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly GamestopDbContext context;

        public PhotoRepository(GamestopDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int gameId)
        {
            return await context.Photos.Where(p => p.GameId == gameId).ToListAsync();
        }

        public async Task<Photo> GetPhoto(int gameId, int id)
        {
            return await context.Photos.Where(p => p.GameId == gameId).SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Remove(Photo photo)
        {
            context.Remove(photo);
        }
    }
}
