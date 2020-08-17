using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CMMS.Infrastructure.Migrations
{
    public partial class FixTypoInFailureOccurredOnProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OccuredOn",
                schema: "maintenance",
                table: "Failures");

            migrationBuilder.AddColumn<DateTime>(
                name: "OccurredOn",
                schema: "maintenance",
                table: "Failures",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OccurredOn",
                schema: "maintenance",
                table: "Failures");

            migrationBuilder.AddColumn<DateTime>(
                name: "OccuredOn",
                schema: "maintenance",
                table: "Failures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
