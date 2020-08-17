using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CMMS.Infrastructure.Migrations
{
    public partial class AddFailure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Failures",
                schema: "maintenance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ResourceId = table.Column<Guid>(nullable: false),
                    WorkerId = table.Column<Guid>(nullable: true),
                    State = table.Column<string>(maxLength: 50, nullable: false),
                    ProblemDescription = table.Column<string>(maxLength: 255, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    OccuredOn = table.Column<DateTime>(nullable: false),
                    ResolvedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Failures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Failures_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "maintenance",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Failures_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalSchema: "maintenance",
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Failures_ResourceId",
                schema: "maintenance",
                table: "Failures",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Failures_WorkerId",
                schema: "maintenance",
                table: "Failures",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Failures",
                schema: "maintenance");
        }
    }
}
