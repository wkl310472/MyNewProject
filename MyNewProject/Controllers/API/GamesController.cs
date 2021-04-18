using Microsoft.AspNetCore.Mvc;
using MyNewProject.Models;
using MyNewProject.Controllers.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNewProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamestopDbContext context;
        private readonly IMapper mapper;

        public GamesController(GamestopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/<GamesController>
        [HttpGet]
        public IEnumerable<GameResource> Get()
        {
            var games = context.Games.Include(g => g.Genres).ToList();
            return mapper.Map<List<Game>,List<GameResource>>(games);
        }

        // GET api/<GamesController>/5
        [HttpGet("{id}")]
        public GameResource Get(int id)
        {
            var game = this.context.Games.Include(g => g.Genres).SingleOrDefault(g => g.Id==id);
            return mapper.Map<Game, GameResource>(game);
        }

        // POST api/<GamesController>
        [HttpPost]
        public IActionResult Post([FromBody] GameResource gameResource)
        {
            var game = mapper.Map<GameResource, Game>(gameResource);
            this.context.Games.Add(game);
            this.context.SaveChanges();

            var result = mapper.Map<Game, GameResource>(game);

            return Ok(result);
        }

        // PUT api/<GamesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] GameResource gameResource)
        {
            var gameInDb = this.context.Games.SingleOrDefault(g => g.Id == id);

            gameInDb = mapper.Map<GameResource, Game>(gameResource);

            this.context.SaveChanges();
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var gameInDb = await this.context.Games.FindAsync(id);

            this.context.Games.Remove(gameInDb);
            this.context.SaveChanges();

            return Ok(id);
        }
    }
}
