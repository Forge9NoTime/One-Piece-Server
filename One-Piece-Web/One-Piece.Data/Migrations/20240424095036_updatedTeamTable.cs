using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace One_Piece.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedTeamTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizerId",
                table: "Teams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OrganizerId",
                table: "Teams",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Organizers_OrganizerId",
                table: "Teams",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Organizers_OrganizerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_OrganizerId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Teams");
        }
    }
}
