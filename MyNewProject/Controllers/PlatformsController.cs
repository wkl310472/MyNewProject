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

namespace MyNewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Platform> platformRepository;
        private readonly IMapper mapper;

        public PlatformsController(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Platform> platformRepository)
        {
            this.unitOfWork = unitOfWork;
            this.platformRepository = platformRepository;
            this.mapper = mapper;
        }

        // GET: api/<PlatformsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var platforms = await platformRepository.Get(includeRelated: false);
            var result = mapper.Map<IEnumerable<Platform>, IEnumerable<KeyValuePairResource>>(platforms);
            return Ok(result);
        }

        // GET api/<PlatformsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var platform = await platformRepository.Get(id, includeRelated: false);
            if (platform == null)
            {
                return NotFound();
            }
            var result = mapper.Map<Platform, KeyValuePairResource>(platform);
            return Ok(result);
        }

        // POST api/<PlatformsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KeyValuePairResource platformResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var platform = mapper.Map<KeyValuePairResource, Platform>(platformResource);
            await platformRepository.Add(platform);
            await unitOfWork.CompleteAsync();

            platform = await platformRepository.Get(platform.Id, includeRelated: false);

            var result = mapper.Map<Platform, KeyValuePairResource>(platform);

            return Ok(result);
        }

        // PUT api/<PlatformsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] KeyValuePairResource platformResource)
        {
            var platformInDb = await platformRepository.Get(id, includeRelated: false);

            if (platformInDb == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(platformResource, platformInDb);

            await unitOfWork.CompleteAsync();

            await platformRepository.Get(platformInDb.Id, includeRelated: false);

            var result = mapper.Map<Platform, KeyValuePairResource>(platformInDb);

            return Ok(result);
        }

        // DELETE api/<PlatformsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var platformInDb = await platformRepository.Get(id, includeRelated: false);
            if (platformInDb == null)
            {
                return NotFound();
            }

            platformRepository.Remove(platformInDb);
            await unitOfWork.CompleteAsync();

            return Ok(platformInDb);
        }
    }
}
