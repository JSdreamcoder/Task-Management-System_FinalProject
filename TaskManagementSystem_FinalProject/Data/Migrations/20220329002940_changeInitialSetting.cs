using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementSystem_FinalProject.Data.Migrations
{
    public partial class changeInitialSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Tasks_TasksId",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "BaseUser");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TasksId",
                table: "Comment",
                newName: "AppTasksId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TasksId",
                table: "Comment",
                newName: "IX_Comment_AppTasksId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Project",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTask_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppTask_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_AppUserId",
                table: "Project",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTask_AppUserId",
                table: "AppTask",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTask_ProjectId",
                table: "AppTask",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AppTask_AppTasksId",
                table: "Comment",
                column: "AppTasksId",
                principalTable: "AppTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_AppUserId",
                table: "Project",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AppTask_AppTasksId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_AppUserId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "AppTask");

            migrationBuilder.DropIndex(
                name: "IX_Project_AppUserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "AppTasksId",
                table: "Comment",
                newName: "TasksId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AppTasksId",
                table: "Comment",
                newName: "IX_Comment_TasksId");

            migrationBuilder.CreateTable(
                name: "BaseUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Tasks_TasksId",
                table: "Comment",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
