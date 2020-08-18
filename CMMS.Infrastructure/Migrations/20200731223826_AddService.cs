using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CMMS.Infrastructure.Migrations
{
    public partial class AddService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                schema: "maintenance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false),
                    ScheduledWorkerId = table.Column<Guid>(nullable: false),
                    ActualWorkerId = table.Column<Guid>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ScheduledStartDateTime = table.Column<DateTime>(nullable: false),
                    ScheduledEndDateTime = table.Column<DateTime>(nullable: false),
                    ActualStartDateTime = table.Column<DateTime>(nullable: true),
                    ActualEndDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Workers_ActualWorkerId",
                        column: x => x.ActualWorkerId,
                        principalSchema: "maintenance",
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "maintenance",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Workers_ScheduledWorkerId",
                        column: x => x.ScheduledWorkerId,
                        principalSchema: "maintenance",
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_ServiceTypes_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "maintenance",
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_ActualWorkerId",
                schema: "maintenance",
                table: "Services",
                column: "ActualWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ResourceId",
                schema: "maintenance",
                table: "Services",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ScheduledWorkerId",
                schema: "maintenance",
                table: "Services",
                column: "ScheduledWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_TypeId",
                schema: "maintenance",
                table: "Services",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services",
                schema: "maintenance");
        }
    }
}
