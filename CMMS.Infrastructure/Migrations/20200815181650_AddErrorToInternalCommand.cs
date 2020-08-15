using Microsoft.EntityFrameworkCore.Migrations;

namespace CMMS.Infrastructure.Migrations
{
    public partial class AddErrorToInternalCommand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                schema: "app",
                name: "Error",
                table: "InternalCommands",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "app",
                table: "OutboxMessages",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                schema: "app",
                name: "Error",
                table: "InternalCommands");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                schema: "app",
                table: "OutboxMessages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
