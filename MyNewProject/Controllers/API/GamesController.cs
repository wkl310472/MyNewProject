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
    public class GamesController : ControllerBase
    {
        private readonly GamestopDbContext context;
        public GamesController(GamestopDbContext context)
        {
            this.context = context;
        }

        // GET: api/<GamesController>
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return this.context.Games.ToList();
        }

        // GET api/<GamesController>/5
        [HttpGet("{id}")]
        public Game Get(int id)
        {
            return this.context.Games.SingleOrDefault(g => g.Id==id);
        }

        // POST api/<GamesController>
        [HttpPost]
        public Game Post([FromBody] Game game)
        {
            this.context.Games.Add(game);
            this.context.SaveChanges();

            return game;
        }

        // PUT api/<GamesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Game game)
        {
            var gameInDb = this.context.Games.SingleOrDefault(g => g.Id == id);

            gameInDb.Name = game.Name;
            gameInDb.Developer = game.Developer;
            gameInDb.Release = game.Release;

            this.context.SaveChanges();
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gameInDb = this.context.Games.SingleOrDefault(g => g.Id == id);

            this.context.Games.Remove(gameInDb);
            this.context.SaveChanges();
        }
    }
}
