using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresentAte.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToPresentationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Presentations",
                newName: "Topic");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Presentations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Presentations",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Presentations");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Presentations");

            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "Presentations",
                newName: "Title");
        }
    }
}
