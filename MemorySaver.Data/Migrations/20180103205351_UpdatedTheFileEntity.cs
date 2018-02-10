using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MemorySaver.Data.Migrations
{
    public partial class UpdatedTheFileEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Files",
                nullable: true);
        }
    }
}
