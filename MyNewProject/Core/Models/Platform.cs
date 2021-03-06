using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyNewProject.Core.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public virtual ICollection<GamePlatform> Games { get; set; }

        public Platform()
        {
            Games = new Collection<GamePlatform>();
        }
    }
}
