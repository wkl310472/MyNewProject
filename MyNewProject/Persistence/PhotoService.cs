using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyNewProject.Core;
using MyNewProject.Core.Models;
using System.Collections.ObjectModel;

namespace MyNewProject.Persistence
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPhotoStorage photoStorage;

        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            this.unitOfWork = unitOfWork;
            this.photoStorage = photoStorage;
        }

        public async Task<IEnumerable<Photo>> UploadPhotos(Game game, IFormCollection files, string uploadsFolderPath)
        {
            var photos = new Collection<Photo>();
            foreach (var file in files.Files)
            {
                var fileName = await photoStorage.StorePhoto(uploadsFolderPath, file);

                var photo = new Photo { FileName = fileName };

                game.Photos.Add(photo);

                photos.Add(photo);
            }

            await unitOfWork.CompleteAsync();

            return photos;
        }

        public async Task RemovePhoto(Game game, Photo photo, string uploadsFolderPath)
        {
            photoStorage.DeletePhoto(uploadsFolderPath, photo.FileName);
            game.Photos.Remove(photo);
            await unitOfWork.CompleteAsync();
        }
    }
}
