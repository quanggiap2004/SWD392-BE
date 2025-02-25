using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class removeboxOptionPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoxOptionPrice",
                table: "BoxOptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "BoxOptionPrice",
                table: "BoxOptions",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
