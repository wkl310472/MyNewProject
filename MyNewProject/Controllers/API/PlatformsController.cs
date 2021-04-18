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
    public class PlatformsController : ControllerBase
    {
        private readonly GamestopDbContext context;

        public PlatformsController(GamestopDbContext context)
        {
            this.context = context;
        }

        // GET: api/<PlatformsController>
        [HttpGet]
        public IEnumerable<Platform> Get()
        {
            return this.context.Platforms.ToList();
        }

        // GET api/<PlatformsController>/5
        [HttpGet("{id}")]
        public Platform Get(int id)
        {
            return this.context.Platforms.SingleOrDefault(p => p.Id == id);
        }

        // POST api/<PlatformsController>
        [HttpPost]
        public Platform Post([FromBody] Platform platform)
        {
            this.context.Platforms.Add(platform);
            this.context.SaveChanges();
            return platform;
        }

        // PUT api/<PlatformsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Platform platform)
        {
            var platformInDb = this.context.Platforms.SingleOrDefault(p => p.Id == id);
            platformInDb.Name = platform.Name;
            this.context.SaveChanges();
        }

        // DELETE api/<PlatformsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var platformInDb = this.context.Platforms.SingleOrDefault(p => p.Id == id);
            this.context.Platforms.Remove(platformInDb);
            this.context.SaveChanges();
        }
    }
}
