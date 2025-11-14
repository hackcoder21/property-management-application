using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserPropertyrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Properties_UserId",
                table: "Properties");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f9c77c44-6b82-4f60-8c74-2d5104696753"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName" },
                values: new object[] { new Guid("d616d64c-ec9c-4556-85e1-ae0867362faa"), "user@user.com", "user" });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Properties_UserId",
                table: "Properties");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d616d64c-ec9c-4556-85e1-ae0867362faa"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName" },
                values: new object[] { new Guid("f9c77c44-6b82-4f60-8c74-2d5104696753"), "user@user.com", "user" });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                table: "Properties",
                column: "UserId",
                unique: true);
        }
    }
}
