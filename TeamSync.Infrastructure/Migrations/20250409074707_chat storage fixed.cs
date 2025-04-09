using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class chatstoragefixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_SenderId",
                table: "ChatMessages");

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

            migrationBuilder.DropColumn(
                name: "From",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "ChatMessages",
                newName: "FromId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_FromId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ProjectId",
                table: "ChatMessages",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Projects_ProjectId",
                table: "ChatMessages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_FromId",
                table: "ChatMessages",
                column: "FromId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Projects_ProjectId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_FromId",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_ProjectId",
                table: "ChatMessages");

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

            migrationBuilder.RenameColumn(
                name: "FromId",
                table: "ChatMessages",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatMessages_FromId",
                table: "ChatMessages",
                newName: "IX_ChatMessages_SenderId");

            migrationBuilder.AddColumn<Guid>(
                name: "From",
                table: "ChatMessages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_SenderId",
                table: "ChatMessages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
