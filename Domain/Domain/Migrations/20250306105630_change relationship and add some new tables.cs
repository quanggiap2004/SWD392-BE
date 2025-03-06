using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlindBoxSystem.Domain.Migrations
{
    /// <inheritdoc />
    public partial class changerelationshipandaddsomenewtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineSerieBoxes_Boxes_BoxId",
                table: "OnlineSerieBoxes");

            migrationBuilder.DropTable(
                name: "RolledItem");

            migrationBuilder.DropIndex(
                name: "IX_OnlineSerieBoxes_BoxId",
                table: "OnlineSerieBoxes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OnlineSerieBoxes");

            migrationBuilder.RenameColumn(
                name: "BoxId",
                table: "OnlineSerieBoxes",
                newName: "PriceIncreasePercent");

            migrationBuilder.AlterColumn<int>(
                name: "OnlineSerieBoxId",
                table: "OnlineSerieBoxes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "MaxTurn",
                table: "OnlineSerieBoxes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterSecret",
                table: "OnlineSerieBoxes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnlineSerieBox",
                table: "BoxOptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CurrentRolledItems",
                columns: table => new
                {
                    CurrentRolledItemId = table.Column<int>(type: "integer", nullable: false),
                    OnlienSeriesBoxId = table.Column<int>(type: "integer", nullable: false),
                    OnlineSerieBoxId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentRolledItems", x => x.CurrentRolledItemId);
                    table.ForeignKey(
                        name: "FK_CurrentRolledItems_BoxItems_CurrentRolledItemId",
                        column: x => x.CurrentRolledItemId,
                        principalTable: "BoxItems",
                        principalColumn: "BoxItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrentRolledItems_OnlineSerieBoxes_OnlineSerieBoxId",
                        column: x => x.OnlineSerieBoxId,
                        principalTable: "OnlineSerieBoxes",
                        principalColumn: "OnlineSerieBoxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRolledItems",
                columns: table => new
                {
                    UserRolledItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OnlineSeriesBoxId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BoxItemId = table.Column<int>(type: "integer", nullable: false),
                    IsCheckOut = table.Column<bool>(type: "boolean", nullable: false),
                    OnlineSerieBoxId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRolledItems", x => x.UserRolledItemId);
                    table.ForeignKey(
                        name: "FK_UserRolledItems_BoxItems_BoxItemId",
                        column: x => x.BoxItemId,
                        principalTable: "BoxItems",
                        principalColumn: "BoxItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRolledItems_OnlineSerieBoxes_OnlineSerieBoxId",
                        column: x => x.OnlineSerieBoxId,
                        principalTable: "OnlineSerieBoxes",
                        principalColumn: "OnlineSerieBoxId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRolledItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentRolledItems_OnlineSerieBoxId",
                table: "CurrentRolledItems",
                column: "OnlineSerieBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolledItems_BoxItemId",
                table: "UserRolledItems",
                column: "BoxItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolledItems_OnlineSerieBoxId",
                table: "UserRolledItems",
                column: "OnlineSerieBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolledItems_UserId",
                table: "UserRolledItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineSerieBoxes_BoxOptions_OnlineSerieBoxId",
                table: "OnlineSerieBoxes",
                column: "OnlineSerieBoxId",
                principalTable: "BoxOptions",
                principalColumn: "BoxOptionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineSerieBoxes_BoxOptions_OnlineSerieBoxId",
                table: "OnlineSerieBoxes");

            migrationBuilder.DropTable(
                name: "CurrentRolledItems");

            migrationBuilder.DropTable(
                name: "UserRolledItems");

            migrationBuilder.DropColumn(
                name: "MaxTurn",
                table: "OnlineSerieBoxes");

            migrationBuilder.DropColumn(
                name: "PriceAfterSecret",
                table: "OnlineSerieBoxes");

            migrationBuilder.DropColumn(
                name: "IsOnlineSerieBox",
                table: "BoxOptions");

            migrationBuilder.RenameColumn(
                name: "PriceIncreasePercent",
                table: "OnlineSerieBoxes",
                newName: "BoxId");

            migrationBuilder.AlterColumn<int>(
                name: "OnlineSerieBoxId",
                table: "OnlineSerieBoxes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "OnlineSerieBoxes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "RolledItem",
                columns: table => new
                {
                    BoxItemsBoxItemId = table.Column<int>(type: "integer", nullable: false),
                    OnlineSerieBoxesOnlineSerieBoxId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolledItem", x => new { x.BoxItemsBoxItemId, x.OnlineSerieBoxesOnlineSerieBoxId });
                    table.ForeignKey(
                        name: "FK_RolledItem_BoxItems_BoxItemsBoxItemId",
                        column: x => x.BoxItemsBoxItemId,
                        principalTable: "BoxItems",
                        principalColumn: "BoxItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolledItem_OnlineSerieBoxes_OnlineSerieBoxesOnlineSerieBoxId",
                        column: x => x.OnlineSerieBoxesOnlineSerieBoxId,
                        principalTable: "OnlineSerieBoxes",
                        principalColumn: "OnlineSerieBoxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSerieBoxes_BoxId",
                table: "OnlineSerieBoxes",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_RolledItem_OnlineSerieBoxesOnlineSerieBoxId",
                table: "RolledItem",
                column: "OnlineSerieBoxesOnlineSerieBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineSerieBoxes_Boxes_BoxId",
                table: "OnlineSerieBoxes",
                column: "BoxId",
                principalTable: "Boxes",
                principalColumn: "BoxId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
