using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CMMS.Infrastructure.Migrations
{
    public partial class AddDescriptionPropertyToServiceAndChangeFKDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Resources_ResourceId",
                schema: "maintenance",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Workers_ScheduledWorkerId",
                schema: "maintenance",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "ScheduledWorkerId",
                schema: "maintenance",
                table: "Services",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "maintenance",
                table: "Services",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Resources_ResourceId",
                schema: "maintenance",
                table: "Services",
                column: "ResourceId",
                principalSchema: "maintenance",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Workers_ScheduledWorkerId",
                schema: "maintenance",
                table: "Services",
                column: "ScheduledWorkerId",
                principalSchema: "maintenance",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Resources_ResourceId",
                schema: "maintenance",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Workers_ScheduledWorkerId",
                schema: "maintenance",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "maintenance",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "ScheduledWorkerId",
                schema: "maintenance",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Resources_ResourceId",
                schema: "maintenance",
                table: "Services",
                column: "ResourceId",
                principalSchema: "maintenance",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Workers_ScheduledWorkerId",
                schema: "maintenance",
                table: "Services",
                column: "ScheduledWorkerId",
                principalSchema: "maintenance",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
