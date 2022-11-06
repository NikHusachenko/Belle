using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Belle.EntityFramework.Migrations
{
    public partial class AppendNullableValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserFK",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "UserFK",
                table: "Products",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "PathToImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 18, 44, 58, 379, DateTimeKind.Local).AddTicks(8955));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 18, 44, 58, 379, DateTimeKind.Local).AddTicks(9011));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 6, 18, 44, 58, 379, DateTimeKind.Local).AddTicks(9013));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserFK",
                table: "Products",
                column: "UserFK",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserFK",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "UserFK",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PathToImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserFK",
                table: "Products",
                column: "UserFK",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
