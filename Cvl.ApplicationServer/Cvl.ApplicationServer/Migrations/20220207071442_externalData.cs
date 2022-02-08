using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cvl.ApplicationServer.Migrations
{
    public partial class externalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessExternalData",
                schema: "Processes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceId = table.Column<long>(type: "bigint", nullable: false),
                    ProcessOutputDataFullSerialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalInputDataFullSerialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessExternalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessExternalData_ProcessInstanceContainer_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalSchema: "Processes",
                        principalTable: "ProcessInstanceContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessExternalData_ProcessInstanceId",
                schema: "Processes",
                table: "ProcessExternalData",
                column: "ProcessInstanceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessExternalData",
                schema: "Processes");
        }
    }
}
