using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PecckosChatProgram.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2024, 1, 31, 12, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2025, 1, 31, 22, 51, 45, 232, DateTimeKind.Utc).AddTicks(3315));
        }
    }
}
