using Microsoft.AspNetCore.Mvc;
using MyNewProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core;
using AutoMapper;
using MyNewProject.Controllers.Resources;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNewProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Genre> genreRepository;
        private readonly IMapper mapper;

        public GenresController(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Genre> genreRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.genreRepository = genreRepository;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var genres = await genreRepository.Get(includeRelated: false);
            var result = mapper.Map<List<Genre>, List<KeyValuePairResource>>(genres);
            return Ok(result);
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var genre = await genreRepository.Get(id, includeRelated: false);
            if (genre == null)
            {
                return NotFound();
            }
            var result = mapper.Map<Genre, KeyValuePairResource>(genre);
            return Ok(result);
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KeyValuePairResource genreResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = mapper.Map<KeyValuePairResource, Genre>(genreResource);
            genreRepository.Add(genre);
            await unitOfWork.CompleteAsync();

            genre = await genreRepository.Get(genre.Id, includeRelated: false);

            var result = mapper.Map<Genre, KeyValuePairResource>(genre);

            return Ok(result);
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] KeyValuePairResource genreResource)
        {
            var genreInDb = await genreRepository.Get(id, includeRelated: false);

            if (genreInDb == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(genreResource, genreInDb);

            await unitOfWork.CompleteAsync();

            await genreRepository.Get(genreInDb.Id, includeRelated: false);

            var result = mapper.Map<Genre, KeyValuePairResource>(genreInDb);

            return Ok(result);
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genreInDb = await genreRepository.Get(id, includeRelated: false);
            if (genreInDb == null)
            {
                return NotFound();
            }

            genreRepository.Remove(genreInDb);
            await unitOfWork.CompleteAsync();

            return Ok(genreInDb);
        }
    }
}
