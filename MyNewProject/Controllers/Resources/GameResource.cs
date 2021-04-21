using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyNewProject.Core.Models;

namespace MyNewProject.Controllers.Resources
{
    public class GameResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Developer { get; set; }

        public DateTime? Release { get; set; }

        public virtual ICollection<KeyValuePairResource> Genres { get; set; }
        public virtual ICollection<KeyValuePairResource> Platforms { get; set; }

        public GameResource()
        {
            Genres = new Collection<KeyValuePairResource>();
            Platforms = new Collection<KeyValuePairResource>();
        }
    }
}
