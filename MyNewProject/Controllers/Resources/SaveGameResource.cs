using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyNewProject.Controllers.Resources
{
    public class SaveGameResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Developer { get; set; }

        [Required]
        public DateTime? Release { get; set; }

        public virtual ICollection<int> Genres { get; set; }
        public virtual ICollection<int> Platforms { get; set; }

        [Range(0.0,999.99)]
        public virtual double Price { get; set; }
        [Range(0,99)]
        public virtual int NumberInStock { get; set; }

        public SaveGameResource()
        {
            Genres = new Collection<int>();
            Platforms = new Collection<int>();
        }
    }
}
