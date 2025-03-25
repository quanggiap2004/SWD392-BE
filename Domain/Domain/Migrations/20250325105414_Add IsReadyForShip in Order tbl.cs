using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadyForShipinOrdertbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsShipOnlineSerieBoxItem",
                table: "Orders",
                newName: "IsReadyForShipBoxItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReadyForShipBoxItem",
                table: "Orders",
                newName: "IsShipOnlineSerieBoxItem");
        }
    }
}
