using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Position__60BB9A59D6236541", x => x.PositionID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    HireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'Active'::character varying"),
                    PositionID = table.Column<long>(type: "bigint", nullable: false),
                    ManagerID = table.Column<long>(type: "bigint", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__7AD04FF1311F0C96", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_Employees_Manager",
                        column: x => x.ManagerID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions",
                        column: x => x.PositionID,
                        principalTable: "Positions",
                        principalColumn: "PositionID");
                });

            migrationBuilder.CreateTable(
                name: "Checkins",
                columns: table => new
                {
                    CheckinID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    CheckinTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Checkins__F3C85D51E292FB00", x => x.CheckinID);
                    table.ForeignKey(
                        name: "FK_Checkins_Employees",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "DailyReports",
                columns: table => new
                {
                    ReportID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalWorkHours = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    WorkStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    GeneralNotes = table.Column<string>(type: "text", nullable: true),
                    IsFinalized = table.Column<bool>(type: "boolean", nullable: false),
                    FinalizedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DailyRep__D5BD48E5FDBC9777", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_DailyReports_Employees",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "DailyWorkSummary",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "date", nullable: false),
                    FirstCheckin = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastCheckout = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TotalCalculatedHours = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    IsFullDay = table.Column<bool>(type: "boolean", nullable: false),
                    IsHalfDay = table.Column<bool>(type: "boolean", nullable: false),
                    IsLateArrival = table.Column<bool>(type: "boolean", nullable: false),
                    IsEarlyDeparture = table.Column<bool>(type: "boolean", nullable: false),
                    IsUnderHours = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DailyWor__3214EC072063D57A", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyWorkSummary_Employees",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => new { x.EmployeeId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesList",
                columns: table => new
                {
                    TaskID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RelatedProject = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AssignedToEmployeeID = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CompletionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'Normal'::character varying"),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'Not Started'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tasks__7C6949D199CAF273", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_EmployeesList_Employees",
                        column: x => x.AssignedToEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "WorkPlans",
                columns: table => new
                {
                    WorkPlanId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    TaskDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PlannedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlans", x => x.WorkPlanId);
                    table.ForeignKey(
                        name: "FK_WorkPlan_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                columns: table => new
                {
                    WorkScheduleID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ScheduleType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WorkSche__C6AC635EC0F1DC14", x => x.WorkScheduleID);
                    table.ForeignKey(
                        name: "FK_WorkSchedules_Employees",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "DailyReport_CompletedTasks",
                columns: table => new
                {
                    ReportCompletedTaskID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportID = table.Column<long>(type: "bigint", nullable: false),
                    TaskID = table.Column<long>(type: "bigint", nullable: false),
                    CompletionDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DailyRep__77D00B9D3609CD06", x => x.ReportCompletedTaskID);
                    table.ForeignKey(
                        name: "FK_ReportCompletedTasks_EmployeesList",
                        column: x => x.TaskID,
                        principalTable: "EmployeesList",
                        principalColumn: "TaskID");
                    table.ForeignKey(
                        name: "FK_ReportCompletedTasks_Report",
                        column: x => x.ReportID,
                        principalTable: "DailyReports",
                        principalColumn: "ReportID");
                });

            migrationBuilder.CreateTable(
                name: "DailyReport_PlannedTasks",
                columns: table => new
                {
                    ReportPlannedTaskID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportID = table.Column<long>(type: "bigint", nullable: false),
                    TaskID = table.Column<long>(type: "bigint", nullable: false),
                    PlannedDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DailyRep__BF13C8980D7F5F7A", x => x.ReportPlannedTaskID);
                    table.ForeignKey(
                        name: "FK_ReportPlannedTasks_EmployeesList",
                        column: x => x.TaskID,
                        principalTable: "EmployeesList",
                        principalColumn: "TaskID");
                    table.ForeignKey(
                        name: "FK_ReportPlannedTasks_Report",
                        column: x => x.ReportID,
                        principalTable: "DailyReports",
                        principalColumn: "ReportID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_EmployeeID",
                table: "Checkins",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_DailyReport_CompletedTasks_TaskID",
                table: "DailyReport_CompletedTasks",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "UQ_ReportCompletedTasks_Report_Task",
                table: "DailyReport_CompletedTasks",
                columns: new[] { "ReportID", "TaskID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyReport_PlannedTasks_TaskID",
                table: "DailyReport_PlannedTasks",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "UQ_ReportPlannedTasks_Report_Task",
                table: "DailyReport_PlannedTasks",
                columns: new[] { "ReportID", "TaskID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_DailyReports_Employee_Date",
                table: "DailyReports",
                columns: new[] { "EmployeeID", "ReportDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyWorkSummary_EmployeeId",
                table: "DailyWorkSummary",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjects_ProjectId",
                table: "EmployeeProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerID",
                table: "Employees",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionID",
                table: "Employees",
                column: "PositionID");

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__A9D1053453A29024",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesList_AssignedToEmployeeID",
                table: "EmployeesList",
                column: "AssignedToEmployeeID");

            migrationBuilder.CreateIndex(
                name: "UQ__Position__E46AEF4299F9F60F",
                table: "Positions",
                column: "PositionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_EmployeeId",
                table: "WorkPlans",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "UQ_WorkSchedules_Employee_Date",
                table: "WorkSchedules",
                columns: new[] { "EmployeeID", "ScheduleDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checkins");

            migrationBuilder.DropTable(
                name: "DailyReport_CompletedTasks");

            migrationBuilder.DropTable(
                name: "DailyReport_PlannedTasks");

            migrationBuilder.DropTable(
                name: "DailyWorkSummary");

            migrationBuilder.DropTable(
                name: "EmployeeProjects");

            migrationBuilder.DropTable(
                name: "WorkPlans");

            migrationBuilder.DropTable(
                name: "WorkSchedules");

            migrationBuilder.DropTable(
                name: "EmployeesList");

            migrationBuilder.DropTable(
                name: "DailyReports");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
