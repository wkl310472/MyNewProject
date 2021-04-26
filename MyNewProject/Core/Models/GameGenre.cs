using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNewProject.Core.Models
{
    [Table("GameGenres")]
    public class GameGenre
    {
        public int GameId { get; set; }
        public int GenreId { get; set; }
        public Game Game { get; set; }
        public Genre Genre { get; set; }
    }
}
