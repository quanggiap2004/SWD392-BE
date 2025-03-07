using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addnewrelationshipbetweenUserRolledItemandOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRolledItemId",
                table: "OrderItems",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_UserRolledItemId",
                table: "OrderItems",
                column: "UserRolledItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_UserRolledItems_UserRolledItemId",
                table: "OrderItems",
                column: "UserRolledItemId",
                principalTable: "UserRolledItems",
                principalColumn: "UserRolledItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_UserRolledItems_UserRolledItemId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_UserRolledItemId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UserRolledItemId",
                table: "OrderItems");
        }
    }
}
