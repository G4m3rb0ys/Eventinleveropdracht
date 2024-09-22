using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eventinleveropdracht.Migrations
{
    /// <inheritdoc />
    public partial class AddclassestoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    CurrentParticipants = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganiserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    AdminCode = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    phone = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservaties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservationNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ammount = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    GuestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservaties_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservaties_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservatieId = table.Column<int>(type: "int", nullable: false),
                    GuestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Reservaties_ReservatieId",
                        column: x => x.ReservatieId,
                        principalTable: "Reservaties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "Password", "Role", "Username", "phone" },
                values: new object[] { 1, "Organizer", "johndoe@example.com", "John Doe", "hashedpassword", "Organizer", "johndoe", 123456789 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "Email", "Name", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 2, "Guest", "test@gmail.com", "Jane Doe", "hashedpassword", "Guest", "janedoe" },
                    { 3, "Guest", "Has@gmail.com", "Jane Doe", "hashedpassword", "Guest", "janedoe2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AdminCode", "Discriminator", "Email", "Name", "Password", "Role", "Username" },
                values: new object[] { 4, 1234, "Admin", "admin@gmail.com", "Admin", "hashedpassword", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Discriminator", "Email", "EventId", "Name", "Password", "Role", "Username" },
                values: new object[] { 5, "Cashier", "cashier@gmail.com", null, "Cashier", "hashedpassword", "Cashier", "Cashier" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CurrentParticipants", "Description", "FromDate", "Image", "Location", "MaxParticipants", "Name", "OrganiserId", "Requirements", "ToDate", "Type" },
                values: new object[] { 2, 50, "This is a beautiful event performing a concert of a well-known DJ", new DateTime(2024, 9, 20, 11, 41, 15, 207, DateTimeKind.Local).AddTicks(5685), "ComingSoon.jpg", "Entire venue", 500, "Test2", 1, "[\"Ticket\",\"ID\"]", new DateTime(2024, 9, 20, 11, 41, 15, 207, DateTimeKind.Local).AddTicks(5737), "Concert" });

            migrationBuilder.InsertData(
                table: "Reservaties",
                columns: new[] { "Id", "Date", "Description", "Email", "EventID", "GuestId", "Name", "Paid", "Price", "ReservationNumber", "ammount", "type" },
                values: new object[] { 1, new DateTime(2024, 9, 20, 11, 41, 15, 207, DateTimeKind.Local).AddTicks(5766), "This is a test", "Testing@gmail.com", 2, 2, "Jane Doe", true, 50, 1234, 2, "VIP" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "GuestId", "ReservatieId", "Status" },
                values: new object[] { 1, new DateTime(2024, 9, 20, 11, 41, 15, 207, DateTimeKind.Local).AddTicks(5784), 2, 1, "Paid" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganiserId",
                table: "Events",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GuestId",
                table: "Orders",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReservatieId",
                table: "Orders",
                column: "ReservatieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaties_EventID",
                table: "Reservaties",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaties_GuestId",
                table: "Reservaties",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EventId",
                table: "Users",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_OrganiserId",
                table: "Events",
                column: "OrganiserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_OrganiserId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Reservaties");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
