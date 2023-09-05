using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITT.EmployeeDb.Migrations
{
    /// <inheritdoc />
    public partial class intis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SequenceGenerator");

            migrationBuilder.CreateSequence(
                name: "Sequence-Generator",
                schema: "SequenceGenerator");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveState = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false),
                    IsReviewer = table.Column<bool>(type: "bit", nullable: false),
                    DevType = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerImage = table.Column<string>(name: "Manager_Image", type: "nvarchar(max)", nullable: true),
                    ProjectManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectType = table.Column<int>(type: "int", nullable: false),
                    Bank = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignedManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedManagers_Employees_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedManagers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeqNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationType = table.Column<int>(type: "int", nullable: false),
                    TaskState = table.Column<int>(type: "int", nullable: false),
                    CompletionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommitTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MainBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MergeBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AssignedManagers_AssignedManagerId",
                        column: x => x.AssignedManagerId,
                        principalTable: "AssignedManagers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssignedBETasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskState = table.Column<int>(type: "int", nullable: false),
                    DevTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeReviews = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedBETasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedBETasks_Employees_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedBETasks_Tasks_DevTaskId",
                        column: x => x.DevTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedQATasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskState = table.Column<int>(type: "int", nullable: false),
                    DevTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QaReviews = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedQATasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedQATasks_Employees_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedQATasks_Employees_QaId",
                        column: x => x.QaId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssignedQATasks_Tasks_DevTaskId",
                        column: x => x.DevTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedBETasks_DeveloperId",
                table: "AssignedBETasks",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedBETasks_DevTaskId",
                table: "AssignedBETasks",
                column: "DevTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedManagers_ProjectId",
                table: "AssignedManagers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedManagers_ProjectManagerId",
                table: "AssignedManagers",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedQATasks_DeveloperId",
                table: "AssignedQATasks",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedQATasks_DevTaskId",
                table: "AssignedQATasks",
                column: "DevTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedQATasks_QaId",
                table: "AssignedQATasks",
                column: "QaId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Bank_ProjectType",
                table: "Projects",
                columns: new[] { "Bank", "ProjectType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedManagerId",
                table: "Tasks",
                column: "AssignedManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedBETasks");

            migrationBuilder.DropTable(
                name: "AssignedQATasks");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "AssignedManagers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropSequence(
                name: "Sequence-Generator",
                schema: "SequenceGenerator");
        }
    }
}
