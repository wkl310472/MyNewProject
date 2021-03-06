using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core.Models;

namespace MyNewProject.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int gameId);
        Task<Photo> GetPhoto(int gameId, int id);
        void Remove(Photo photo);
    }
}
