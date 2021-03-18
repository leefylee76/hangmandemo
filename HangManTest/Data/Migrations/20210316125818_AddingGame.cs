using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HangMan.Data.Migrations
{
    public partial class AddingGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(nullable: true),
                    Word = table.Column<string>(nullable: true),
                    Lives = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    DatePlayed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
