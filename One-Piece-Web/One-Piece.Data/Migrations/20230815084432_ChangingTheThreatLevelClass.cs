using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace One_Piece.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTheThreatLevelClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThreatLevel",
                table: "Missions",
                newName: "MissionThreatLevelId");

            migrationBuilder.CreateTable(
                name: "MissionThreatLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionThreatLevel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Missions_MissionThreatLevelId",
                table: "Missions",
                column: "MissionThreatLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_MissionThreatLevel_MissionThreatLevelId",
                table: "Missions",
                column: "MissionThreatLevelId",
                principalTable: "MissionThreatLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_MissionThreatLevel_MissionThreatLevelId",
                table: "Missions");

            migrationBuilder.DropTable(
                name: "MissionThreatLevel");

            migrationBuilder.DropIndex(
                name: "IX_Missions_MissionThreatLevelId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "MissionThreatLevelId",
                table: "Missions",
                newName: "ThreatLevel");
        }
    }
}
