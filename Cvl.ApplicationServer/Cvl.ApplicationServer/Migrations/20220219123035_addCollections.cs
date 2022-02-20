using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cvl.ApplicationServer.Migrations
{
    public partial class addCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessStepHistory_ProcessInstanceContainer_ProcessInstanceId",
                schema: "Processes",
                table: "ProcessStepHistory");

            migrationBuilder.RenameColumn(
                name: "ProcessInstanceId",
                schema: "Processes",
                table: "ProcessStepHistory",
                newName: "ProcessInstanceContainerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessStepHistory_ProcessInstanceId",
                schema: "Processes",
                table: "ProcessStepHistory",
                newName: "IX_ProcessStepHistory_ProcessInstanceContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessStepHistory_ProcessInstanceContainer_ProcessInstanceContainerId",
                schema: "Processes",
                table: "ProcessStepHistory",
                column: "ProcessInstanceContainerId",
                principalSchema: "Processes",
                principalTable: "ProcessInstanceContainer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessStepHistory_ProcessInstanceContainer_ProcessInstanceContainerId",
                schema: "Processes",
                table: "ProcessStepHistory");

            migrationBuilder.RenameColumn(
                name: "ProcessInstanceContainerId",
                schema: "Processes",
                table: "ProcessStepHistory",
                newName: "ProcessInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessStepHistory_ProcessInstanceContainerId",
                schema: "Processes",
                table: "ProcessStepHistory",
                newName: "IX_ProcessStepHistory_ProcessInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessStepHistory_ProcessInstanceContainer_ProcessInstanceId",
                schema: "Processes",
                table: "ProcessStepHistory",
                column: "ProcessInstanceId",
                principalSchema: "Processes",
                principalTable: "ProcessInstanceContainer",
                principalColumn: "Id");
        }
    }
}
