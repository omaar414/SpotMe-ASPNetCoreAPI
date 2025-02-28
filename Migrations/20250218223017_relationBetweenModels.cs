using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotMeAPI.Migrations
{
    /// <inheritdoc />
    public partial class relationBetweenModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSent",
                table: "FriendRequests",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSent",
                table: "FriendRequests");
        }
    }
}
