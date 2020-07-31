using Microsoft.EntityFrameworkCore.Migrations;

namespace CMMS.Infrastructure.Migrations
{
    public partial class AddIsUniqueIndexToServiceTypesNameProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_Name",
                schema: "maintenance",
                table: "ServiceTypes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceTypes_Name",
                schema: "maintenance",
                table: "ServiceTypes");
        }
    }
}
