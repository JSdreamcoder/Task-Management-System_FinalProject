using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem_FinalProject.Data.Migrations
{
    public partial class addDailySalaryInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailySalary",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailySalary",
                table: "AspNetUsers");
        }
    }
}
