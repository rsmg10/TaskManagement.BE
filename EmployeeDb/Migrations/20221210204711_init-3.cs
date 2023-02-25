using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITT.EmployeeDb.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedManagers_Managers_ProjectManagerId",
                table: "AssignedManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedManagers_Projects_ProjectId",
                table: "AssignedManagers");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "QAReviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "BEReviews",
                type: "nvarchar(max)",
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

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedManagers_Managers_ProjectManagerId",
                table: "AssignedManagers",
                column: "ProjectManagerId",
                principalTable: "Managers",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedManagers_Managers_ProjectManagerId",
                table: "AssignedManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedManagers_Projects_ProjectId",
                table: "AssignedManagers");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "QAReviews");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "BEReviews");

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
        }
    }
}