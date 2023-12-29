using System;
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
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3801), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3810) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3811), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3812) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3813), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3813) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3815), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3816) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3817), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3817) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3818), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3818) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3819), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3819) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3820), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3820) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3821), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3821) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3822), new DateTime(2023, 12, 18, 11, 17, 31, 249, DateTimeKind.Local).AddTicks(3822) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1731), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1739) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1742), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1742) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1743), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1744) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1745), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1746) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1747), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1747) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1748), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1748) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1749), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1749) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1750), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1751) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1751), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1752) });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "TagID",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1753), new DateTime(2023, 12, 14, 17, 2, 5, 608, DateTimeKind.Local).AddTicks(1753) });
        }
    }
}
