using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiLog.Migrations
{
    /// <inheritdoc />
    public partial class TagAssignmentDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TagAssignedDateTime",
                table: "Visitors",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagAssignedDateTime",
                table: "Visitors");
        }
    }
}
