using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem_FinalProject.Data.Migrations
{
    public partial class setNullabletoAppUserIdInAppTaskModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTask_AspNetUsers_AppUserId",
                table: "AppTask");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "AppTask",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTask_AspNetUsers_AppUserId",
                table: "AppTask",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTask_AspNetUsers_AppUserId",
                table: "AppTask");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "AppTask",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTask_AspNetUsers_AppUserId",
                table: "AppTask",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
