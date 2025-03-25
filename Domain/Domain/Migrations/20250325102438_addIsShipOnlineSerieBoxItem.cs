using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addIsShipOnlineSerieBoxItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOnlineSerieBoxOrder",
                table: "Orders",
                newName: "IsShipOnlineSerieBoxItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsShipOnlineSerieBoxItem",
                table: "Orders",
                newName: "IsOnlineSerieBoxOrder");
        }
    }
}
