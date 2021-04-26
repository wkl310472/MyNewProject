using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core.Models;

namespace MyNewProject.Core
{
    public interface IRepository<T>
    {
        Task<List<T>> Get(bool includeRelated = true);
        Task<T> Get(int id, bool includeRelated = true);
        void Add(T item);
        void Remove(T item);
    }
}
