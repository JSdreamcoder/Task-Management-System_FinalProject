using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem_FinalProject.Data.Migrations
{
    public partial class AddDeadLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLine",
                table: "AppTask",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadLine",
                table: "AppTask");
        }
    }
}
