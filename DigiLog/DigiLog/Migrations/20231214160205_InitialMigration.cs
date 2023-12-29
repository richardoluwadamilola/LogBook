using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigiLog.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Department = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TagNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MiddleName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReasonForVisit = table.Column<int>(type: "int", nullable: false),
                    ReasonForVisitDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Photo = table.Column<byte[]>(type: "longblob", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitors_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagID", "DateCreated", "DateModified", "Deleted", "TagNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1731), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1739), false, "VIS-A60" },
                    { 2, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1742), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1742), false, "VIS-A61" },
                    { 3, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1743), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1744), false, "VIS-A62" },
                    { 4, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1745), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1746), false, "VIS-A78" },
                    { 5, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1747), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1747), false, "VIS-A66" },
                    { 6, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1748), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1748), false, "VIS-A56" },
                    { 7, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1749), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1749), false, "VIS-A87" },
                    { 8, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1750), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1751), false, "VIS-A78" },
                    { 9, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1751), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1752), false, "VIS-A88" },
                    { 10, new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1753), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1753), false, "VIS-A48" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_EmployeeId",
                table: "Visitors",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
