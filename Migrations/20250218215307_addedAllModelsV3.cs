using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotMeAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedAllModelsV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_friendRequests_Users_ReceiverId",
                table: "friendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_friendRequests_Users_SenderId",
                table: "friendRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_friendRequests",
                table: "friendRequests");

            migrationBuilder.RenameTable(
                name: "friendRequests",
                newName: "FriendRequests");

            migrationBuilder.RenameIndex(
                name: "IX_friendRequests_SenderId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_friendRequests_ReceiverId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_ReceiverId",
                table: "FriendRequests",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_SenderId",
                table: "FriendRequests",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_ReceiverId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_SenderId",
                table: "FriendRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.RenameTable(
                name: "FriendRequests",
                newName: "friendRequests");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_SenderId",
                table: "friendRequests",
                newName: "IX_friendRequests_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_ReceiverId",
                table: "friendRequests",
                newName: "IX_friendRequests_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_friendRequests",
                table: "friendRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_friendRequests_Users_ReceiverId",
                table: "friendRequests",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_friendRequests_Users_SenderId",
                table: "friendRequests",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
