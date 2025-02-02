using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PecckosChatProgram.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$q2cYg.eqK.4hzoEsGbXt1.rKOVjNiiJZTDWD0oNs4hsZ0QoghL7oO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2025, 2, 2, 21, 43, 26, 747, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$znTIKf1mVe.U4ZtwyOiq1eNojXa.yQHzsqdE8aqWIQusiKp2AxOFW");
        }
    }
}
