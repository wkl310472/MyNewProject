using Microsoft.AspNetCore.Mvc;
using MyNewProject.Core.Models;
using MyNewProject.Core;
using MyNewProject.Controllers.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyNewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IRepository<Game> gameRepository;

        public GamesController(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Game> gameRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.gameRepository = gameRepository;
        }

        // GET: api/<GamesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var games = await gameRepository.Get();
            var result = mapper.Map<IEnumerable<Game>, IEnumerable<GameResource>>(games);
            return Ok(result);
        }

        // GET api/<GamesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var game = await gameRepository.Get(id);
            if (game == null)
            {
                return NotFound();
            }
            var result = mapper.Map<Game, GameResource>(game);
            return Ok(result);
        }

        // POST api/<GamesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveGameResource gameResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var game = mapper.Map<SaveGameResource, Game>(gameResource);

            gameRepository.Add(game);

            await unitOfWork.CompleteAsync();

            game = await gameRepository.Get(game.Id);

            var result = mapper.Map<Game, GameResource>(game);

            return Ok(result);
        }

        // PUT api/<GamesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SaveGameResource saveGameResource)
        {
            //throw new Exception();
            var gameInDb = await gameRepository.Get(id);

            if (gameInDb == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(saveGameResource, gameInDb);

            await unitOfWork.CompleteAsync();

            await gameRepository.Get(gameInDb.Id);

            var result = mapper.Map<Game, GameResource>(gameInDb);

            return Ok(result);
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var gameInDb = await gameRepository.Get(id, includeRelated: false);
            if (gameInDb == null)
            {
                return NotFound();
            }

            gameRepository.Remove(gameInDb);
            await unitOfWork.CompleteAsync();

            return Ok(gameInDb);
        }
    }
}
