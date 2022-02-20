using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cvl.ApplicationServer.Migrations
{
    public partial class processType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "Processes",
                table: "ProcessInstanceContainer",
                newName: "ProcessTypeData_ProcessTypeFullName");

            migrationBuilder.AddColumn<int>(
                name: "ProcessTypeData_ProcessType",
                schema: "Processes",
                table: "ProcessInstanceContainer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessTypeData_ProcessType",
                schema: "Processes",
                table: "ProcessInstanceContainer");

            migrationBuilder.RenameColumn(
                name: "ProcessTypeData_ProcessTypeFullName",
                schema: "Processes",
                table: "ProcessInstanceContainer",
                newName: "Type");
        }
    }
}
