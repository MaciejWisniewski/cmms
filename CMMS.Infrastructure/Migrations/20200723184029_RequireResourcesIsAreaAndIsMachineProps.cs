using Microsoft.EntityFrameworkCore.Migrations;

namespace CMMS.Infrastructure.Migrations
{
    public partial class RequireResourcesIsAreaAndIsMachineProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsMachine",
                schema: "maintenance",
                table: "Resources",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsArea",
                schema: "maintenance",
                table: "Resources",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsMachine",
                schema: "maintenance",
                table: "Resources",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsArea",
                schema: "maintenance",
                table: "Resources",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
