using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem_FinalProject.Data.Migrations
{
    public partial class adduseridInNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AppUserId",
                table: "Notification",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_AppUserId",
                table: "Notification",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_AppUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_AppUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Notification");
        }
    }
}
