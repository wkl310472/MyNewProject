using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNewProject.Migrations
{
    public partial class seedGamesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Games (Name,Developer,Release) VALUES ('Genshin Impact','miHoYo','2020-09-25')");
            migrationBuilder.Sql("INSERT INTO Games (Name,Developer,Release) VALUES ('Hades','Supergiant Games','2020-09-17')");
            migrationBuilder.Sql("INSERT INTO Games (Name,Developer,Release) VALUES ('League of Legends','Riot Games','2009-10-27')");
            migrationBuilder.Sql("INSERT INTO Games (Name,Developer,Release) VALUES ('Devil May Cry','Capcom','2001-08-23')");
            migrationBuilder.Sql("INSERT INTO Games (Name,Developer,Release) VALUES ('World of Warcraft','Blizzard Entertainment','2004-11-23')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Games WHERE Id = (SELECT Id FROM GAMES WHERE Name = 'Genshin Impact')");
            migrationBuilder.Sql("DELETE FROM Games WHERE Id = (SELECT Id FROM GAMES WHERE Name = 'Hades')");
            migrationBuilder.Sql("DELETE FROM Games WHERE Id = (SELECT Id FROM GAMES WHERE Name = 'League of Legends')");
            migrationBuilder.Sql("DELETE FROM Games WHERE Id = (SELECT Id FROM GAMES WHERE Name = 'Devil May Cry')");
            migrationBuilder.Sql("DELETE FROM Games WHERE Id = (SELECT Id FROM GAMES WHERE Name = 'World of Warcraft')");
        }
    }
}
