using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chatstorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProjectRoles",
                columns: new[] { "Id", "ProjectRoleName" },
                values: new object[,]
                {
                    { new Guid("2dbf4035-46bb-4e49-a95e-a368809c14bc"), "Member" },
                    { new Guid("6a92fe5a-a9b1-475f-9b87-0ff77225aaf5"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "TaskStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { new Guid("36050345-8e45-46a8-9cd2-15b522b9df7b"), "Pending" },
                    { new Guid("c09440d1-28d6-4297-b779-d60ada25f304"), "InWork" },
                    { new Guid("e975ae0e-9c2a-4084-8fb1-52ef3a0d08d3"), "Done" },
                    { new Guid("f6a303ff-67d2-4cb1-9de4-eb268fc503b5"), "Closed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("2dbf4035-46bb-4e49-a95e-a368809c14bc"));

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("6a92fe5a-a9b1-475f-9b87-0ff77225aaf5"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("36050345-8e45-46a8-9cd2-15b522b9df7b"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("c09440d1-28d6-4297-b779-d60ada25f304"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("e975ae0e-9c2a-4084-8fb1-52ef3a0d08d3"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("f6a303ff-67d2-4cb1-9de4-eb268fc503b5"));

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
    }
}
