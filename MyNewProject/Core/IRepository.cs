using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core.Models;

namespace MyNewProject.Core
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get(Filter filter = null,bool includeRelated = true);
        Task<T> Get(int id, bool includeRelated = true);
        Task Add(T item);
        void Remove(T item);
    }
}
