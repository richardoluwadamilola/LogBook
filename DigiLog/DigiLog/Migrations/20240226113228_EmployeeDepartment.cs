using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiLog.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "Visitors",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "Visitors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Visitors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Visitors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_DepartmentId",
                table: "Visitors",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Departments_DepartmentId",
                table: "Visitors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Departments_DepartmentId",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_DepartmentId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Visitors");
        }
    }
}
