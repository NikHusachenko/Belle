using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Belle.EntityFramework.Migrations
{
    public partial class AddedEmailToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedOn", "Email" },
                values: new object[] { new DateTime(2022, 11, 5, 22, 48, 3, 357, DateTimeKind.Local).AddTicks(1090), "vadimEmail@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedOn", "Email" },
                values: new object[] { new DateTime(2022, 11, 5, 22, 48, 3, 357, DateTimeKind.Local).AddTicks(1125), "vasiaEmail@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedOn", "Email" },
                values: new object[] { new DateTime(2022, 11, 5, 22, 48, 3, 357, DateTimeKind.Local).AddTicks(1128), "petya.super.email@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 5, 19, 26, 45, 630, DateTimeKind.Local).AddTicks(3124));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 5, 19, 26, 45, 630, DateTimeKind.Local).AddTicks(3163));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedOn",
                value: new DateTime(2022, 11, 5, 19, 26, 45, 630, DateTimeKind.Local).AddTicks(3166));
        }
    }
}
