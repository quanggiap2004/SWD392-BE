using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class removevarianttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_BoxVariants_BoxVariantId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "BoxVariants");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.AddColumn<int>(
                name: "BoxOptionId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BoxOptions",
                columns: table => new
                {
                    BoxOptionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BoxId = table.Column<int>(type: "integer", nullable: false),
                    BoxOptionName = table.Column<string>(type: "text", nullable: false),
                    BoxOptionPrice = table.Column<float>(type: "real", nullable: false),
                    OriginPrice = table.Column<float>(type: "real", nullable: false),
                    DisplayPrice = table.Column<float>(type: "real", nullable: false),
                    BoxOptionStock = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxOptions", x => x.BoxOptionId);
                    table.ForeignKey(
                        name: "FK_BoxOptions_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "BoxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxOptions_BoxId",
                table: "BoxOptions",
                column: "BoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_BoxOptions_BoxVariantId",
                table: "OrderItems",
                column: "BoxVariantId",
                principalTable: "BoxOptions",
                principalColumn: "BoxOptionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_BoxOptions_BoxVariantId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "BoxOptions");

            migrationBuilder.DropColumn(
                name: "BoxOptionId",
                table: "OrderItems");

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VariantName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.VariantId);
                });

            migrationBuilder.CreateTable(
                name: "BoxVariants",
                columns: table => new
                {
                    BoxVariantId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BoxId = table.Column<int>(type: "integer", nullable: false),
                    VariantId = table.Column<int>(type: "integer", nullable: false),
                    BoxVariantName = table.Column<string>(type: "text", nullable: false),
                    BoxVariantPrice = table.Column<float>(type: "real", nullable: false),
                    BoxVariantStock = table.Column<int>(type: "integer", nullable: false),
                    DisplayPrice = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    OriginPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxVariants", x => x.BoxVariantId);
                    table.ForeignKey(
                        name: "FK_BoxVariants_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "BoxId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoxVariants_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxVariants_BoxId",
                table: "BoxVariants",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_BoxVariants_VariantId",
                table: "BoxVariants",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_BoxVariants_BoxVariantId",
                table: "OrderItems",
                column: "BoxVariantId",
                principalTable: "BoxVariants",
                principalColumn: "BoxVariantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
