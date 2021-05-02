using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core.Models;
using Microsoft.AspNetCore.Http;

namespace MyNewProject.Core
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> UploadPhotos(Game game, IFormCollection files, string uploadsFolderPath);
        Task RemovePhoto(Game game, Photo photo, string uploadsFolderPath);
    }
}
