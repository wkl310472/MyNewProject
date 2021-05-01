using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MyNewProject.Core.Models;
using MyNewProject.Core;
using AutoMapper;
using MyNewProject.Controllers.Resources;
using Microsoft.Extensions.Options;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNewProject.Controllers
{
    [Route("api/games/{gameId}/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IWebHostEnvironment host;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IRepository<Game> gameRepository;
        private readonly IPhotoRepository photoRepository;
        private readonly PhotoSettings photoSettings;

        public PhotosController(IWebHostEnvironment host, IUnitOfWork unitOfWork, IMapper mapper,IRepository<Game> gameRepository, IPhotoRepository photoRepository, IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoSettings = options.Value;
            this.host = host;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.gameRepository = gameRepository;
            this.photoRepository = photoRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int gameId,int id)
        {
            var photo = await photoRepository.GetPhoto(gameId,id);
            if (photo == null)
            {
                return NotFound();
            }
            var result = mapper.Map<Photo, PhotoResource>(photo);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> Get(int gameId)
        {
            var photos = await photoRepository.GetPhotos(gameId);
            var result = mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
            return Ok(result);
        }

        // POST api/games/id/<PhotosController>
        [HttpPost]
        public async Task<IActionResult> Post(int gameId, IFormFile file) 
        {
            var game = await gameRepository.Get(gameId, includeRelated: false);
            if (game == null)
            {
                return NotFound();
            }

            if (file == null)
            {
                return BadRequest("Null file.");
            }
            if (file.Length == 0)
            {
                return BadRequest("Empty file.");
            }
            if (file.Length > photoSettings.MaxBytes)
            {
                return BadRequest("Max file size exceeded.");
            }
            if (!photoSettings.isSupported(file.FileName))
            {
                return BadRequest("Invalid file type.");
            }

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

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

            var photo = new Photo { FileName = fileName };

            game.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<Photo, PhotoResource>(photo);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int gameId, int id)
        {
            var photoInDb = await photoRepository.GetPhoto(gameId, id);
            if (photoInDb == null)
            {
                return NotFound();
            }

            photoRepository.Remove(photoInDb);
            await unitOfWork.CompleteAsync();

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadsFolderPath, photoInDb.FileName);
            if (!Directory.Exists(uploadsFolderPath) || !System.IO.File.Exists(filePath))
            {
                return Ok("The photo has already been deleted");
            }
            System.IO.File.Delete(filePath);

            var result = mapper.Map<Photo, PhotoResource>(photoInDb);
            return Ok(result);
        }
    }
}
