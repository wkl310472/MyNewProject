using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyNewProject.Controllers.Resources
{
    public class GenreResource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<KeyValuePairResource> Games { get; set; }

        public GenreResource()
        {
            this.Games = new Collection<KeyValuePairResource>();
        }

    }
}
