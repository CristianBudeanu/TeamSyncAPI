using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NotificationFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("a6406290-0c29-407d-950c-3046984bfa0a"));

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("ac32235c-a2b8-40ee-9d42-b4f3283968af"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("2963d058-f152-456b-97ad-2fd7e06e27a3"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("805612c5-4345-4680-8ddf-dbe38026e7f3"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("a851761c-99b9-4ae3-8e0d-8a5304b5aecd"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("e3799698-6611-4a3d-bf0e-06c0967cbb8a"));

            migrationBuilder.CreateTable(
                name: "ChatNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatNotifications", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProjectRoles",
                columns: new[] { "Id", "ProjectRoleName" },
                values: new object[,]
                {
                    { new Guid("00ebde90-6f75-45c7-b78e-89f3ed485bff"), "Member" },
                    { new Guid("793d7f3b-fa08-40fd-ba1f-41adec179ecc"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "TaskStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { new Guid("3c90a6c1-af5b-4c29-b84b-44e25a25f8b2"), "Done" },
                    { new Guid("427e94e2-499d-4909-8f3c-ecc7b56c4602"), "Closed" },
                    { new Guid("826cb250-43e8-455d-ae77-e3d82c6b55bc"), "Pending" },
                    { new Guid("860e9698-2a1a-44c2-be81-3ddd54dd03c5"), "InWork" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatNotifications");

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("00ebde90-6f75-45c7-b78e-89f3ed485bff"));

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: new Guid("793d7f3b-fa08-40fd-ba1f-41adec179ecc"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("3c90a6c1-af5b-4c29-b84b-44e25a25f8b2"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("427e94e2-499d-4909-8f3c-ecc7b56c4602"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("826cb250-43e8-455d-ae77-e3d82c6b55bc"));

            migrationBuilder.DeleteData(
                table: "TaskStatuses",
                keyColumn: "Id",
                keyValue: new Guid("860e9698-2a1a-44c2-be81-3ddd54dd03c5"));

            migrationBuilder.InsertData(
                table: "ProjectRoles",
                columns: new[] { "Id", "ProjectRoleName" },
                values: new object[,]
                {
                    { new Guid("a6406290-0c29-407d-950c-3046984bfa0a"), "Member" },
                    { new Guid("ac32235c-a2b8-40ee-9d42-b4f3283968af"), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "TaskStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { new Guid("2963d058-f152-456b-97ad-2fd7e06e27a3"), "InWork" },
                    { new Guid("805612c5-4345-4680-8ddf-dbe38026e7f3"), "Pending" },
                    { new Guid("a851761c-99b9-4ae3-8e0d-8a5304b5aecd"), "Done" },
                    { new Guid("e3799698-6611-4a3d-bf0e-06c0967cbb8a"), "Closed" }
                });
        }
    }
}
