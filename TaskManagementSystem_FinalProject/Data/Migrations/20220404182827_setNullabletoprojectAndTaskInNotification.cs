using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem_FinalProject.Data.Migrations
{
    public partial class setNullabletoprojectAndTaskInNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AppTask_AppTaskId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Project_ProjectId",
                table: "Notification");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Notification",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AppTaskId",
                table: "Notification",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AppTask_AppTaskId",
                table: "Notification",
                column: "AppTaskId",
                principalTable: "AppTask",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Project_ProjectId",
                table: "Notification",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AppTask_AppTaskId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Project_ProjectId",
                table: "Notification");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppTaskId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AppTask_AppTaskId",
                table: "Notification",
                column: "AppTaskId",
                principalTable: "AppTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Project_ProjectId",
                table: "Notification",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
