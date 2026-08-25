using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskCategoryentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_categories_CategoryId",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_CategoryId",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "tasks");

            migrationBuilder.CreateTable(
                name: "task_categories",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_categories", x => new { x.TaskId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_task_categories_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_task_categories_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_task_categories_CategoryId",
                table: "task_categories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task_categories");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_CategoryId",
                table: "tasks",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_categories_CategoryId",
                table: "tasks",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
