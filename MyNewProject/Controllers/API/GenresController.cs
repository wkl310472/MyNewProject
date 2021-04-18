using Microsoft.AspNetCore.Mvc;
using MyNewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNewProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GamestopDbContext context;

        public GenresController(GamestopDbContext context)
        {
            this.context = context;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public IEnumerable<Genre> Get()
        {
            return this.context.Genres.ToList();
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public Genre Get(int id)
        {
            return this.context.Genres.SingleOrDefault(g => g.Id == id);
        }

        // POST api/<GenresController>
        [HttpPost]
        public Genre Post([FromBody] Genre genre)
        {
            this.context.Genres.Add(genre);
            this.context.SaveChanges();
            return genre;
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Genre genre)
        {
            var genreInDb = this.context.Genres.SingleOrDefault(g => g.Id == id);
            genreInDb.Name = genre.Name;
            this.context.SaveChanges();
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var genreInDb = this.context.Genres.SingleOrDefault(g => g.Id == id);
            this.context.Genres.Remove(genreInDb);
            this.context.SaveChanges();
        }
    }
}
