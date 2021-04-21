using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core;

namespace MyNewProject.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GamestopDbContext context;

        public UnitOfWork(GamestopDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
