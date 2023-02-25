using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITT.EmployeeDb.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedManagers_ProjectManagers_ProjectManagerId",
                table: "AssignedManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedManagers_Projects_ProjectId",
                table: "AssignedManagers");

            migrationBuilder.DropTable(
                name: "ProjectManagers");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AssignedTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DevTasks",
                table: "DevTasks");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "DevTasks");

            migrationBuilder.RenameTable(
                name: "DevTasks",
                newName: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Pin",
                table: "Developers",
                newName: "Last");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "Developers",
                newName: "First");

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityId",
                table: "Developers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectManagerId",
                table: "AssignedManagers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "AssignedManagers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AssignedManagers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedManagerId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssignedBETasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskState = table.Column<int>(type: "int", nullable: false),
                    DevTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedBETasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedBETasks_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedQATasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedQATasks_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedQATasks_Tasks_DevTaskId",
                        column: x => x.DevTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BEReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedBETaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Findings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BEReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BEReviews_AssignedBETasks_AssignedBETaskId",
                        column: x => x.AssignedBETaskId,
                        principalTable: "AssignedBETasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QAReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedQATaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Findings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QAReviews_AssignedQATasks_AssignedQATaskId",
                        column: x => x.AssignedQATaskId,
                        principalTable: "AssignedQATasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    First = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Identities_IdentityId",
                        column: x => x.IdentityId,
                        principalTable: "Identities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Developers_IdentityId",
                table: "Developers",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedManagerId",
                table: "Tasks",
                column: "AssignedManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedBETasks_DeveloperId",
                table: "AssignedBETasks",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedBETasks_DevTaskId",
                table: "AssignedBETasks",
                column: "DevTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedQATasks_DeveloperId",
                table: "AssignedQATasks",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedQATasks_DevTaskId",
                table: "AssignedQATasks",
                column: "DevTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_BEReviews_AssignedBETaskId",
                table: "BEReviews",
                column: "AssignedBETaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdentityId",
                table: "Managers",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_QAReviews_AssignedQATaskId",
                table: "QAReviews",
                column: "AssignedQATaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedManagers_Managers_ProjectManagerId",
                table: "AssignedManagers",
                column: "ProjectManagerId",
                principalTable: "Managers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedManagers_Projects_ProjectId",
                table: "AssignedManagers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Identities_IdentityId",
                table: "Developers",
                column: "IdentityId",
                principalTable: "Identities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AssignedManagers_AssignedManagerId",
                table: "Tasks",
                column: "AssignedManagerId",
                principalTable: "AssignedManagers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedManagers_Managers_ProjectManagerId",
                table: "AssignedManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedManagers_Projects_ProjectId",
                table: "AssignedManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Identities_IdentityId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AssignedManagers_AssignedManagerId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "BEReviews");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "QAReviews");

            migrationBuilder.DropTable(
                name: "AssignedBETasks");

            migrationBuilder.DropTable(
                name: "Identities");

            migrationBuilder.DropTable(
                name: "AssignedQATasks");

            migrationBuilder.DropIndex(
                name: "IX_Developers_IdentityId",
                table: "Developers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedManagerId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AssignedManagers");

            migrationBuilder.DropColumn(
                name: "AssignedManagerId",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "DevTasks");

            migrationBuilder.RenameColumn(
                name: "Last",
                table: "Developers",
                newName: "Pin");

            migrationBuilder.RenameColumn(
                name: "First",
                table: "Developers",
                newName: "NickName");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Developers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Developers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectManagerId",
                table: "AssignedManagers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "AssignedManagers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "DevTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DevTasks",
                table: "DevTasks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssignedTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DevTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedTasks_AssignedManagers_AssignedManagerId",
                        column: x => x.AssignedManagerId,
                        principalTable: "AssignedManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedTasks_DevTasks_DevTaskId",
                        column: x => x.DevTaskId,
                        principalTable: "DevTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedTasks_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActiveState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Findings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AssignedTasks_AssignedTaskId",
                        column: x => x.AssignedTaskId,
                        principalTable: "AssignedTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_AssignedManagerId",
                table: "AssignedTasks",
                column: "AssignedManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_DeveloperId",
                table: "AssignedTasks",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_DevTaskId",
                table: "AssignedTasks",
                column: "DevTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AssignedTaskId",
                table: "Reviews",
                column: "AssignedTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedManagers_ProjectManagers_ProjectManagerId",
                table: "AssignedManagers",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedManagers_Projects_ProjectId",
                table: "AssignedManagers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}