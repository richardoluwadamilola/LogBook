using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiLog.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Tags_TagNumber",
                table: "Visitors");

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Visitors",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Tags_TagNumber",
                table: "Visitors",
                column: "TagNumber",
                principalTable: "Tags",
                principalColumn: "TagNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Tags_TagNumber",
                table: "Visitors");

            migrationBuilder.UpdateData(
                table: "Visitors",
                keyColumn: "TagNumber",
                keyValue: null,
                column: "TagNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Visitors",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Tags_TagNumber",
                table: "Visitors",
                column: "TagNumber",
                principalTable: "Tags",
                principalColumn: "TagNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
