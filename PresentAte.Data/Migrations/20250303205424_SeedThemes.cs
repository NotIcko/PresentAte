using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PresentAte.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedThemes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EssayThemes",
                columns: new[] { "ThemeId", "ThemeName" },
                values: new object[,]
                {
                    { 1, "Мълчанието - сила или безсилие" },
                    { 2, "Живот в мрежата" },
                    { 3, "Тежестта на думите" },
                    { 4, "Смисълът на благодеянието - Йордан Йовков \" Песента на колелетата \"" },
                    { 5, "Съдбовният избор на човека - Йордан Йовков \" Шибил \"" },
                    { 6, "Човекът и неговите съмнения - Елин Пелин \" Чорба от греховете на отец Никодим \"" },
                    { 7, "Родното и чуждото - Алеко Константинов \" Бай Ганьо \"" },
                    { 8, "Реалност и измислица - Елин Пелин \" Косачи \"" },
                    { 9, "Доброта и самота - Йордан Йовков \" Серафим \"" },
                    { 10, "Човекът и вярата - Никола Вапцаров \" Вяра \"" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EssayThemes",
                keyColumn: "ThemeId",
                keyValue: 10);
        }
    }
}
