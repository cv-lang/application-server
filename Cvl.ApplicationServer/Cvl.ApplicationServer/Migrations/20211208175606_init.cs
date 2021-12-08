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
                name: "Temporary");

            migrationBuilder.EnsureSchema(
                name: "Processes");

            migrationBuilder.CreateTable(
                name: "LogElement",
                schema: "Temporary",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionNumber = table.Column<int>(type: "int", nullable: false),
                    ParentNumber = table.Column<int>(type: "int", nullable: true),
                    ProcessId = table.Column<long>(type: "bigint", nullable: false),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogElement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessActivityData",
                schema: "Processes",
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
                    table.PrimaryKey("PK_ProcessActivityData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessInstanceContainer",
                schema: "Processes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThreadData_MainThreadState = table.Column<int>(type: "int", nullable: false),
                    ThreadData_NextExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BusinessData_ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessData_VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessData_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessData_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Step_Step = table.Column<int>(type: "int", nullable: true),
                    Step_StepName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Step_StepDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalIds_ExternalId1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExternalIds_ExternalId2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExternalIds_ExternalId3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExternalIds_ExternalId4 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProcessSpecificData_ProcessSpecificData1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessSpecificData_ProcessSpecificData2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInstanceContainer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessActivity",
                schema: "Processes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProcessActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessActivity_ProcessActivityData_ProcessActivityDataId",
                        column: x => x.ProcessActivityDataId,
                        principalSchema: "Processes",
                        principalTable: "ProcessActivityData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessActivity_ProcessInstanceContainer_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalSchema: "Processes",
                        principalTable: "ProcessInstanceContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessDiagnosticData",
                schema: "Processes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceId = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfActivities = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfSteps = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfErrors = table.Column<long>(type: "bigint", nullable: false),
                    LastError = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastErrorPreview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRequestPreview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastResponsePreview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessDiagnosticData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessDiagnosticData_ProcessInstanceContainer_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalSchema: "Processes",
                        principalTable: "ProcessInstanceContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessStateData",
                schema: "Processes",
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
                    table.PrimaryKey("PK_ProcessStateData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessStateData_ProcessInstanceContainer_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalSchema: "Processes",
                        principalTable: "ProcessInstanceContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessStepHistory",
                schema: "Processes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceId = table.Column<long>(type: "bigint", nullable: true),
                    Step_Step = table.Column<int>(type: "int", nullable: true),
                    Step_StepName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Step_StepDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archival = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessStepHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessStepHistory_ProcessInstanceContainer_ProcessInstanceId",
                        column: x => x.ProcessInstanceId,
                        principalSchema: "Processes",
                        principalTable: "ProcessInstanceContainer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActivity_ProcessActivityDataId",
                schema: "Processes",
                table: "ProcessActivity",
                column: "ProcessActivityDataId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessActivity_ProcessInstanceId",
                schema: "Processes",
                table: "ProcessActivity",
                column: "ProcessInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDiagnosticData_ProcessInstanceId",
                schema: "Processes",
                table: "ProcessDiagnosticData",
                column: "ProcessInstanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInstanceContainer_ProcessNumber",
                schema: "Processes",
                table: "ProcessInstanceContainer",
                column: "ProcessNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStateData_ProcessInstanceId",
                schema: "Processes",
                table: "ProcessStateData",
                column: "ProcessInstanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStepHistory_ProcessInstanceId",
                schema: "Processes",
                table: "ProcessStepHistory",
                column: "ProcessInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogElement",
                schema: "Temporary");

            migrationBuilder.DropTable(
                name: "ProcessActivity",
                schema: "Processes");

            migrationBuilder.DropTable(
                name: "ProcessDiagnosticData",
                schema: "Processes");

            migrationBuilder.DropTable(
                name: "ProcessStateData",
                schema: "Processes");

            migrationBuilder.DropTable(
                name: "ProcessStepHistory",
                schema: "Processes");

            migrationBuilder.DropTable(
                name: "ProcessActivityData",
                schema: "Processes");

            migrationBuilder.DropTable(
                name: "ProcessInstanceContainer",
                schema: "Processes");
        }
    }
}
