using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigiLog.Migrations
{
    /// <inheritdoc />
    public partial class ReasonForVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonForVisit",
                table: "Visitors");

            migrationBuilder.AddColumn<long>(
                name: "ReasonForVisitId",
                table: "Visitors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ReasonForVisit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Reason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonForVisit", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ReasonForVisit",
                columns: new[] { "Id", "Reason" },
                values: new object[,]
                {
                    { 1L, "Official" },
                    { 2L, "Personal" },
                    { 3L, "Interview" },
                    { 4L, "Delivery" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_ReasonForVisitId",
                table: "Visitors",
                column: "ReasonForVisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_ReasonForVisit_ReasonForVisitId",
                table: "Visitors",
                column: "ReasonForVisitId",
                principalTable: "ReasonForVisit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_ReasonForVisit_ReasonForVisitId",
                table: "Visitors");

            migrationBuilder.DropTable(
                name: "ReasonForVisit");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_ReasonForVisitId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "ReasonForVisitId",
                table: "Visitors");

            migrationBuilder.AddColumn<string>(
                name: "ReasonForVisit",
                table: "Visitors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
