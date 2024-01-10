using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DigiLog.Migrations
{
    /// <inheritdoc />
    public partial class LongMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 10);

            migrationBuilder.AlterColumn<long>(
                name: "TagID",
                table: "Visitors",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "EmployeeId",
                table: "Visitors",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Visitors",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "TagID",
                table: "Tags",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Employees",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagID", "DateCreated", "DateModified", "Deleted", "TagNumber" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5003), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5012), false, "VIS-A60" },
                    { 2L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5013), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5014), false, "VIS-A61" },
                    { 3L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5015), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5016), false, "VIS-A62" },
                    { 4L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5016), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5017), false, "VIS-A78" },
                    { 5L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5018), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5018), false, "VIS-A66" },
                    { 6L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5019), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5019), false, "VIS-A56" },
                    { 7L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5020), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5021), false, "VIS-A87" },
                    { 8L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5022), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5022), false, "VIS-A78" },
                    { 9L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5023), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5023), false, "VIS-A88" },
                    { 10L, new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5024), new DateTime(2024, 1, 10, 15, 34, 18, 124, DateTimeKind.Local).AddTicks(5024), false, "VIS-A48" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 10L);

            migrationBuilder.AlterColumn<int>(
                name: "TagID",
                table: "Visitors",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Visitors",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Visitors",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagID",
                table: "Tags",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagID", "DateCreated", "DateModified", "Deleted", "TagNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3801), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3810), false, "VIS-A60" },
                    { 2, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3811), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3812), false, "VIS-A61" },
                    { 3, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3813), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3813), false, "VIS-A62" },
                    { 4, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3815), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3816), false, "VIS-A78" },
                    { 5, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3817), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3817), false, "VIS-A66" },
                    { 6, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3818), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3818), false, "VIS-A56" },
                    { 7, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3819), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3819), false, "VIS-A87" },
                    { 8, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3820), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3820), false, "VIS-A78" },
                    { 9, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3821), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3821), false, "VIS-A88" },
                    { 10, new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3822), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3822), false, "VIS-A48" }
                });
        }
    }
}
