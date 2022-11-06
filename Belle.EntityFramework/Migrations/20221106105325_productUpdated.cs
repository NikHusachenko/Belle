using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Belle.EntityFramework.Migrations
{
    public partial class productUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PathToImage",
                table: "Products",
                newName: "Details");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductImageEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathToImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductFK = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImageEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImageEntity_Products_ProductFK",
                        column: x => x.ProductFK,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 12, 53, 24, 728, DateTimeKind.Local).AddTicks(6860));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 12, 53, 24, 728, DateTimeKind.Local).AddTicks(6892));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 12, 53, 24, 728, DateTimeKind.Local).AddTicks(6894));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageEntity_ProductFK",
                table: "ProductImageEntity",
                column: "ProductFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImageEntity");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Products",
                newName: "PathToImage");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 5, 22, 48, 3, 357, DateTimeKind.Local).AddTicks(1090));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 5, 22, 48, 3, 357, DateTimeKind.Local).AddTicks(1125));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 5, 22, 48, 3, 357, DateTimeKind.Local).AddTicks(1128));
        }
    }
}
