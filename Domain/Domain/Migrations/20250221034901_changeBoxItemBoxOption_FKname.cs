using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class changeBoxItemBoxOption_FKname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_BoxOptions_BoxVariantId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_BoxVariantId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BoxVariantId",
                table: "OrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BoxOptionId",
                table: "OrderItems",
                column: "BoxOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_BoxOptions_BoxOptionId",
                table: "OrderItems",
                column: "BoxOptionId",
                principalTable: "BoxOptions",
                principalColumn: "BoxOptionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_BoxOptions_BoxOptionId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_BoxOptionId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "BoxVariantId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BoxVariantId",
                table: "OrderItems",
                column: "BoxVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_BoxOptions_BoxVariantId",
                table: "OrderItems",
                column: "BoxVariantId",
                principalTable: "BoxOptions",
                principalColumn: "BoxOptionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
