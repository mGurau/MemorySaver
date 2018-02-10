using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MemorySaver.Data.Migrations
{
    public partial class Created_Proper_Navigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chests_Users_UserId",
                table: "Chests");

            migrationBuilder.DropIndex(
                name: "IX_Chests_UserId",
                table: "Chests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chests");

            migrationBuilder.CreateIndex(
                name: "IX_Chests_OwnerId",
                table: "Chests",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chests_Users_OwnerId",
                table: "Chests",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chests_Users_OwnerId",
                table: "Chests");

            migrationBuilder.DropIndex(
                name: "IX_Chests_OwnerId",
                table: "Chests");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Chests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chests_UserId",
                table: "Chests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chests_Users_UserId",
                table: "Chests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
