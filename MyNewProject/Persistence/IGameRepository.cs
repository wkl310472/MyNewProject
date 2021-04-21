using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Models;

namespace MyNewProject.Persistence
{
    public interface IGameRepository
    {
        Task<List<Game>> Get(bool includeRelated = true);
        Task<Game> Get(int id, bool includeRelated = true);
        void Add(Game game);
        void Remove(Game game);
    }
}
