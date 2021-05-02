﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyNewProject.Core;

namespace MyNewProject.Persistence
{
    public class FileSystemPhotoStorage : IPhotoStorage
    {
        public async Task<string> StorePhoto(string uploadsFolderPath, IFormFile file)
        {
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName).ToLower();
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public void DeletePhoto(string uploadsFolderPath, string fileName)
        {
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            if (!Directory.Exists(uploadsFolderPath) || !File.Exists(filePath))
            {
                return;
            }
            File.Delete(filePath);
        }
    }
}
