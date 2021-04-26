using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyNewProject.Core.Models
{
    [Table("GamePlatforms")]
    public class GamePlatform
    {
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        public Game Game { get; set; }
        public Platform Platform { get; set; }
    }
}
