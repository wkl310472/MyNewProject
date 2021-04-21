using Microsoft.AspNetCore.Mvc;
using MyNewProject.Models;
using MyNewProject.Controllers.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyNewProject.Persistence;

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
        public async Task<IActionResult> Get()
        {
            var games = await context.Games.Include(g => g.Genres).Include(g => g.Platforms).ToListAsync();
            var result = mapper.Map<List<Game>, List<GameResource>>(games);
            return Ok(result);
        }

        // GET api/<GamesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var game = await this.context.Games.Include(g => g.Genres).Include(g => g.Platforms).SingleOrDefaultAsync(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            var result = mapper.Map<Game, GameResource>(game);
            return Ok(result);
        }

        // POST api/<GamesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameResource gameResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var game = mapper.Map<GameResource, Game>(gameResource);
            this.context.Games.Add(game);
            await this.context.SaveChangesAsync();

            var result = mapper.Map<Game, GameResource>(game);

            return Ok(result);
        }

        // PUT api/<GamesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GameResource gameResource)
        {
            var gameInDb = await this.context.Games.Include(g => g.Genres).Include(g => g.Platforms).SingleOrDefaultAsync(g => g.Id == id);

            if (gameInDb == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map<GameResource, Game>(gameResource,gameInDb);

            await this.context.SaveChangesAsync();

            var result = mapper.Map<Game, GameResource>(gameInDb);

            return Ok(result);
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var gameInDb = await this.context.Games.FindAsync(id);
            if (gameInDb == null)
            {
                return NotFound();
            }

            this.context.Games.Remove(gameInDb);
            await this.context.SaveChangesAsync();

            return Ok(gameInDb);
        }
    }
}
