using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OriginalPublication",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "DateOfDeath", "FirstName", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1, new DateTime(1802, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1885, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Victor", "Hugo", "" },
                    { 2, new DateTime(1926, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nelle", "Lee", "Harper" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address1", "Address2", "City", "State", "Title", "Zip" },
                values: new object[,]
                {
                    { 1, "", "", "New York", 31, "Harper Perennial", "" },
                    { 2, "", "", "New York", 31, "Signet Classes", "" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Format", "Isbn", "OriginalPublication", "PageCount", "PublisherId", "Title" },
                values: new object[] { 2, 2, 1, "0062420704", new DateTime(1960, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 336, 1, "To Kill a Mockingbird" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Format", "Isbn", "OriginalPublication", "PageCount", "PublisherId", "Title" },
                values: new object[] { 1, 1, 0, "045141943X", new DateTime(1862, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1488, 2, "Les Misérables" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "OriginalPublication",
                table: "Books");
        }
    }
}
