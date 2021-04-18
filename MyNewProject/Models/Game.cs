using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyNewProject.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Developer { get; set; }

        [Required]
        public DateTime Release { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Platform> Platforms { get; set; }
    }
}
