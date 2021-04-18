using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNewProject.Migrations
{
    public partial class seedPlatformsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('Microsoft Windows')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('macOS')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('PlayStation')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('Android')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('iOS')");
            migrationBuilder.Sql("INSERT INTO Platforms (Name) VALUES ('Nintendo Switch')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Platforms WHERE Id = (SELECT Id FROM Platforms WHERE Name='Microsoft Windows')");
            migrationBuilder.Sql("DELETE FROM Platforms WHERE Id = (SELECT Id FROM Platforms WHERE Name='macOS')");
            migrationBuilder.Sql("DELETE FROM Platforms WHERE Id = (SELECT Id FROM Platforms WHERE Name='PlayStation')");
            migrationBuilder.Sql("DELETE FROM Platforms WHERE Id = (SELECT Id FROM Platforms WHERE Name='Android')");
            migrationBuilder.Sql("DELETE FROM Platforms WHERE Id = (SELECT Id FROM Platforms WHERE Name='iOS')");
            migrationBuilder.Sql("DELETE FROM Platforms WHERE Id = (SELECT Id FROM Platforms WHERE Name='Nintendo Switch')");
        }
    }
}
