using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventinleveropdracht.Migrations
{
    /// <inheritdoc />
    public partial class editedreservatie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reservaties");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FromDate", "ToDate" },
                values: new object[] { new DateTime(2024, 10, 11, 10, 27, 41, 518, DateTimeKind.Local).AddTicks(5572), new DateTime(2024, 10, 11, 10, 27, 41, 518, DateTimeKind.Local).AddTicks(5637) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 11, 10, 27, 41, 518, DateTimeKind.Local).AddTicks(5679));

            migrationBuilder.UpdateData(
                table: "Reservaties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 11, 10, 27, 41, 518, DateTimeKind.Local).AddTicks(5663));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reservaties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FromDate", "ToDate" },
                values: new object[] { new DateTime(2024, 10, 10, 12, 0, 52, 606, DateTimeKind.Local).AddTicks(3132), new DateTime(2024, 10, 10, 12, 0, 52, 606, DateTimeKind.Local).AddTicks(3183) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 10, 12, 0, 52, 606, DateTimeKind.Local).AddTicks(3233));

            migrationBuilder.UpdateData(
                table: "Reservaties",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 10, 10, 12, 0, 52, 606, DateTimeKind.Local).AddTicks(3211), "This is a test" });
        }
    }
}
