using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class addColumnIntoOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentOrderStatusName",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CurrentOrderStatusId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentOrderStatusId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CurrentOrderStatusName",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
