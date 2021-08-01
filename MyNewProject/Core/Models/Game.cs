using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace MyNewProject.Core.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Developer { get; set; }

        [Required]
        public DateTime? Release { get; set; }

        public virtual ICollection<GameGenre> Genres { get; set; }
        public virtual ICollection<GamePlatform> Platforms { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual double Price { get; set; }
        public virtual int NumberInStock { get; set; }

        public Game()
        {
            Genres = new Collection<GameGenre>();
            Platforms = new Collection<GamePlatform>();
            Photos = new Collection<Photo>();
        }

        public override string ToString()
        {
            string result = "";
            result += this.Name;
            result += " ";

            var genres = Genres.Select(g => g.GenreId.ToString()).ToList();
            result += "[" + String.Join(",", genres) + "]" + " ";
            var platforms = Platforms.Select(p => p.PlatformId.ToString()).ToList();
            result += "[" + String.Join(",", platforms) + "]";

            return result;
        }
    }
}
