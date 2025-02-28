using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotMeAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeLocationModelV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Users_UserId",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "UsersLocations");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_UserId",
                table: "UsersLocations",
                newName: "IX_UsersLocations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersLocations",
                table: "UsersLocations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLocations_Users_UserId",
                table: "UsersLocations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersLocations_Users_UserId",
                table: "UsersLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersLocations",
                table: "UsersLocations");

            migrationBuilder.RenameTable(
                name: "UsersLocations",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_UsersLocations_UserId",
                table: "Locations",
                newName: "IX_Locations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Users_UserId",
                table: "Locations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
