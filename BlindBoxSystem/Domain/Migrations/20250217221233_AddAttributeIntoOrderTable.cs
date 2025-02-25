using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeIntoOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OpenRequest",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RefundRequest",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenRequest",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RefundRequest",
                table: "Orders");
        }
    }
}
