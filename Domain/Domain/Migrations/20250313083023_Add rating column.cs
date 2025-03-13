using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Addratingcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackType",
                table: "Feedbacks");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Feedbacks",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "BoxOptions",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Boxes",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "BoxOptions");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Boxes");

            migrationBuilder.AddColumn<string>(
                name: "FeedbackType",
                table: "Feedbacks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
