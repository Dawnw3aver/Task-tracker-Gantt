using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tasks.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskName = table.Column<string>(type: "TEXT", nullable: false),
                    Resource = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PercentComplete = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "EndDate", "PercentComplete", "Resource", "StartDate", "TaskName" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Задачи", new DateTime(2022, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Работа 1" },
                    { 2, new DateTime(2023, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 70, "Задачи", new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Работа 2" },
                    { 3, new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 90, "Задачи", new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Работа 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
