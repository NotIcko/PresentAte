using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresentAte.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEssays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EssayThemes",
                columns: table => new
                {
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThemeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayThemes", x => x.ThemeId);
                });

            migrationBuilder.CreateTable(
                name: "Essays",
                columns: table => new
                {
                    EssayId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Essays", x => x.EssayId);
                    table.ForeignKey(
                        name: "FK_Essays_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Essays_EssayThemes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "EssayThemes",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EssaySuggestions",
                columns: table => new
                {
                    SuggestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuggestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EssayId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssaySuggestions", x => x.SuggestionId);
                    table.ForeignKey(
                        name: "FK_EssaySuggestions_Essays_EssayId",
                        column: x => x.EssayId,
                        principalTable: "Essays",
                        principalColumn: "EssayId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Essays_ThemeId",
                table: "Essays",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Essays_UserId",
                table: "Essays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EssaySuggestions_EssayId",
                table: "EssaySuggestions",
                column: "EssayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EssaySuggestions");

            migrationBuilder.DropTable(
                name: "Essays");

            migrationBuilder.DropTable(
                name: "EssayThemes");
        }
    }
}
