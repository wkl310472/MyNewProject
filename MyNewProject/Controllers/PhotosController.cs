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
        private readonly IMapper mapper;
        private readonly IRepository<Game> gameRepository;
        private readonly IPhotoRepository photoRepository;
        private readonly IPhotoService photoService;
        private readonly PhotoSettings photoSettings;

        public PhotosController(IWebHostEnvironment host, 
            IMapper mapper,
            IRepository<Game> gameRepository, 
            IPhotoRepository photoRepository, 
            IPhotoService photoService,
            IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoSettings = options.Value;
            this.photoService = photoService;
            this.host = host;
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
        public async Task<IActionResult> Post(int gameId, IFormCollection files) 
        {
            var game = await gameRepository.Get(gameId, includeRelated: false);
            if (game == null)
            {
                return NotFound();
            }

            if (files.Files.Count == 0) 
            {
                return BadRequest("No files uploaded.");
            }

            foreach (var file in files.Files)
            {
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
            }

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

            var photos = await photoService.UploadPhotos(game, files, uploadsFolderPath);

            var result = mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int gameId, int id)
        {
            var game = await gameRepository.Get(gameId, includeRelated: true);

            var photo = game.Photos.SingleOrDefault(p => p.Id == id);
            if (photo == null)
            {
                return NotFound();
            }

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

            await photoService.RemovePhoto(game, photo, uploadsFolderPath);

            var result = mapper.Map<Photo, PhotoResource>(photo);
            return Ok(result);
        }
    }
}
