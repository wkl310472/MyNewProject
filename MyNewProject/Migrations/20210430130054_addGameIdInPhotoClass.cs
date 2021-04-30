using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNewProject.Migrations
{
    public partial class addGameIdInPhotoClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Games_GameId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Games_GameId",
                table: "Photos",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Games_GameId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Photos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Games_GameId",
                table: "Photos",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
