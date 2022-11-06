using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Belle.EntityFramework.Migrations
{
    public partial class ProductCanKeepOneImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImageEntity");

            migrationBuilder.AddColumn<string>(
                name: "PathToImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 15, 17, 32, 646, DateTimeKind.Local).AddTicks(947));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 15, 17, 32, 646, DateTimeKind.Local).AddTicks(980));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 15, 17, 32, 646, DateTimeKind.Local).AddTicks(982));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathToImage",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductImageEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductFK = table.Column<long>(type: "bigint", nullable: false),
                    PathToImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
