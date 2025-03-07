using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdaterelationshipbetweenCurrentRolledItemandBoxItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentRolledItems_BoxItems_CurrentRolledItemId",
                table: "CurrentRolledItems");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentRolledItemId",
                table: "CurrentRolledItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "BoxItemId",
                table: "CurrentRolledItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentRolledItems_BoxItemId",
                table: "CurrentRolledItems",
                column: "BoxItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentRolledItems_BoxItems_BoxItemId",
                table: "CurrentRolledItems",
                column: "BoxItemId",
                principalTable: "BoxItems",
                principalColumn: "BoxItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentRolledItems_BoxItems_BoxItemId",
                table: "CurrentRolledItems");

            migrationBuilder.DropIndex(
                name: "IX_CurrentRolledItems_BoxItemId",
                table: "CurrentRolledItems");

            migrationBuilder.DropColumn(
                name: "BoxItemId",
                table: "CurrentRolledItems");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentRolledItemId",
                table: "CurrentRolledItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentRolledItems_BoxItems_CurrentRolledItemId",
                table: "CurrentRolledItems",
                column: "CurrentRolledItemId",
                principalTable: "BoxItems",
                principalColumn: "BoxItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
