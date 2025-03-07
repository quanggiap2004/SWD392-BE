using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddBasePriceValuetoonlineseriebox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineSeriesBoxId",
                table: "UserRolledItems");

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                table: "OnlineSerieBoxes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "OnlineSerieBoxes");

            migrationBuilder.AddColumn<int>(
                name: "OnlineSeriesBoxId",
                table: "UserRolledItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
