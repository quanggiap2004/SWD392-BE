using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addimageurltoonlineseriebox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "OnlineSerieBoxes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "OnlineSerieBoxes");
        }
    }
}
