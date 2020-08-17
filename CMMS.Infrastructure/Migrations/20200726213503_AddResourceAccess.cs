using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CMMS.Infrastructure.Migrations
{
    public partial class AddResourceAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceAccesses",
                schema: "maintenance",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(nullable: false),
                    WorkerId = table.Column<Guid>(nullable: false),
                    GivenOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceAccesses", x => new { x.ResourceId, x.WorkerId });
                    table.ForeignKey(
                        name: "FK_ResourceAccesses_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "maintenance",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceAccesses_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalSchema: "maintenance",
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAccesses_WorkerId",
                schema: "maintenance",
                table: "ResourceAccesses",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceAccesses",
                schema: "maintenance");
        }
    }
}
