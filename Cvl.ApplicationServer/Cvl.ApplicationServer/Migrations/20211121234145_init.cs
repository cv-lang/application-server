using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cvl.ApplicationServer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Processes");

            migrationBuilder.CreateTable(
                name: "ProcessActivityDaties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestFullSerialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseFullSerialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessActivityDaties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessInstance",
                schema: "Processes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Step = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StepDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainThreadState = table.Column<int>(type: "int", nullable: false),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceId = table.Column<long>(type: "bigint", nullable: true),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientIpPort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientConnectionData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityState = table.Column<int>(type: "int", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviewRequestJson = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviewResponseJson = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ProcessActivityDataId = table.Column<long>(type: "bigint", nullable: false),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessActivities_ProcessActivityDaties_ProcessActivityDataId",
                        column: x => x.ProcessActivityDataId,
                        principalTable: "ProcessActivityDaties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessActivities_ProcessInstance_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalSchema: "Processes",
                        principalTable: "ProcessInstance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessStates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceId = table.Column<long>(type: "bigint", nullable: false),
                    ProcessStateFullSerialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessStates_ProcessInstance_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalSchema: "Processes",
                        principalTable: "ProcessInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StepHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceId = table.Column<long>(type: "bigint", nullable: true),
                    Step = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StepDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepHistories_ProcessInstance_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalSchema: "Processes",
                        principalTable: "ProcessInstance",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActivities_ProcessActivityDataId",
                table: "ProcessActivities",
                column: "ProcessActivityDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActivities_ProcessInstanceId",
                table: "ProcessActivities",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStates_ProcessInstanceId",
                table: "ProcessStates",
                column: "ProcessInstanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StepHistories_ProcessInstanceId",
                table: "StepHistories",
                column: "ProcessInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessActivities");

            migrationBuilder.DropTable(
                name: "ProcessStates");

            migrationBuilder.DropTable(
                name: "StepHistories");

            migrationBuilder.DropTable(
                name: "ProcessActivityDaties");

            migrationBuilder.DropTable(
                name: "ProcessInstance",
                schema: "Processes");
        }
    }
}
