using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddRefundStatuscolumninOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRefund",
                table: "OrderItems");

            migrationBuilder.AddColumn<string>(
                name: "RefundStatus",
                table: "OrderItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundStatus",
                table: "OrderItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsRefund",
                table: "OrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
