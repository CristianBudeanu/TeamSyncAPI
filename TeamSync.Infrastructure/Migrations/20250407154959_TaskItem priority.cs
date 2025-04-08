using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaskItempriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("4297b324-059e-4ae6-a99d-7ebee3e5b731"));

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("c4ada97f-cefb-4dc6-9e5c-f1a9040f701f"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("08cb7114-b9a2-4ad1-83de-eabdbab2ee79"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("444673d3-4b78-40c4-b2ac-8173b2128434"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("50ac0d58-2258-4661-b908-8d26ff9ab1f2"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("b4a299cc-677c-4ad5-a39b-d8cd1470066f"));

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ProjectRoles",
                columns: new[] { "Id", "ProjectRoleName" },
                values: new object[,]
                {
                    { new Guid("c40ecf32-7fcb-4049-aca9-848d6642e17b"), "Member" },
                    { new Guid("e4fa2d4d-6500-4d4f-b75f-84be3b6293f5"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "TaskStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { new Guid("2f7de79c-1300-468e-8f5d-0b3e90c2d7d4"), "Closed" },
                    { new Guid("73712a4d-96dd-47b0-a2cc-8b39601a6eae"), "Pending" },
                    { new Guid("8e749c8b-e52a-4edb-bfae-f6675055243b"), "Done" },
                    { new Guid("a2bb74b9-0a0d-44ac-94b8-b4b3763dccfc"), "InWork" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("c40ecf32-7fcb-4049-aca9-848d6642e17b"));

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("e4fa2d4d-6500-4d4f-b75f-84be3b6293f5"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("2f7de79c-1300-468e-8f5d-0b3e90c2d7d4"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("73712a4d-96dd-47b0-a2cc-8b39601a6eae"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("8e749c8b-e52a-4edb-bfae-f6675055243b"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("a2bb74b9-0a0d-44ac-94b8-b4b3763dccfc"));

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tasks");

            migrationBuilder.InsertData(
                table: "ProjectRoles",
                columns: new[] { "Id", "ProjectRoleName" },
                values: new object[,]
                {
                    { new Guid("4297b324-059e-4ae6-a99d-7ebee3e5b731"), "Administrator" },
                    { new Guid("c4ada97f-cefb-4dc6-9e5c-f1a9040f701f"), "Member" }
                });

            migrationBuilder.InsertData(
                table: "TaskStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { new Guid("08cb7114-b9a2-4ad1-83de-eabdbab2ee79"), "Pending" },
                    { new Guid("444673d3-4b78-40c4-b2ac-8173b2128434"), "Done" },
                    { new Guid("50ac0d58-2258-4661-b908-8d26ff9ab1f2"), "InWork" },
                    { new Guid("b4a299cc-677c-4ad5-a39b-d8cd1470066f"), "Closed" }
                });
        }
    }
}
