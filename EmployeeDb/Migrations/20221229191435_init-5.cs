using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MITT.EmployeeDb.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Identities_IdentityId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Identities_IdentityId",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "Identities");

            migrationBuilder.DropIndex(
                name: "IX_Managers_IdentityId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Developers_IdentityId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Developers");

            migrationBuilder.AddColumn<bool>(
                name: "IsSigned",
                table: "Managers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSigned",
                table: "Developers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSigned",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "IsSigned",
                table: "Developers");

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityId",
                table: "Managers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdentityId",
                table: "Developers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Identities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdentityId",
                table: "Managers",
                column: "IdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Developers_IdentityId",
                table: "Developers",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Identities_IdentityId",
                table: "Developers",
                column: "IdentityId",
                principalTable: "Identities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Identities_IdentityId",
                table: "Managers",
                column: "IdentityId",
                principalTable: "Identities",
                principalColumn: "Id");
        }
    }
}
