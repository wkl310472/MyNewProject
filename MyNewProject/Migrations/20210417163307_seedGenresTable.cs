using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNewProject.Migrations
{
    public partial class seedGenresTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Action')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('MOBA')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Roguelike')");
            migrationBuilder.Sql("INSERT INTO Genres (Name) VALUES ('Role-playing')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Genres WHERE Id = (SELECT Id FROM Genres WHERE Name='Action')");
            migrationBuilder.Sql("DELETE FROM Genres WHERE Id = (SELECT Id FROM Genres WHERE Name='MOBA')");
            migrationBuilder.Sql("DELETE FROM Genres WHERE Id = (SELECT Id FROM Genres WHERE Name='Roguelike')");
            migrationBuilder.Sql("DELETE FROM Genres WHERE Id = (SELECT Id FROM Genres WHERE Name='Role-playing')");
        }
    }
}
