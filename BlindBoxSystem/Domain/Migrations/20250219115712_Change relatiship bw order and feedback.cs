using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Changerelatishipbworderandfeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_OrderItemId",
                table: "Feedbacks");

            migrationBuilder.AddColumn<int>(
                name: "NumOfVoucher",
                table: "Vouchers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_OrderItemId",
                table: "Feedbacks",
                column: "OrderItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_OrderItemId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "NumOfVoucher",
                table: "Vouchers");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_OrderItemId",
                table: "Feedbacks",
                column: "OrderItemId");
        }
    }
}
