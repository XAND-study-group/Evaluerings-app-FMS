using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.Feedback.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FeedbackModule");

            migrationBuilder.CreateTable(
                name: "ClassId",
                schema: "FeedbackModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassIdValue = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUserId",
                schema: "FeedbackModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIdValue = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUserId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "FeedbackModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassIdRoom",
                schema: "FeedbackModule",
                columns: table => new
                {
                    ClassIdsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassIdRoom", x => new { x.ClassIdsId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_ClassIdRoom_ClassId_ClassIdsId",
                        column: x => x.ClassIdsId,
                        principalSchema: "FeedbackModule",
                        principalTable: "ClassId",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassIdRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "FeedbackModule",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                schema: "FeedbackModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    NotificationStatus = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HashedUserId_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Problem_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solution_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "FeedbackModule",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUserIdRoom",
                schema: "FeedbackModule",
                columns: table => new
                {
                    NotificationSubscribedUserIdsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUserIdRoom", x => new { x.NotificationSubscribedUserIdsId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_NotificationUserIdRoom_NotificationUserId_NotificationSubscribedUserIdsId",
                        column: x => x.NotificationSubscribedUserIdsId,
                        principalSchema: "FeedbackModule",
                        principalTable: "NotificationUserId",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationUserIdRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "FeedbackModule",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "FeedbackModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentText_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "FeedbackModule",
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalSchema: "FeedbackModule",
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                schema: "FeedbackModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteScale = table.Column<int>(type: "int", nullable: false),
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HashedUserId_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalSchema: "FeedbackModule",
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassIdRoom_RoomId",
                schema: "FeedbackModule",
                table: "ClassIdRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId",
                schema: "FeedbackModule",
                table: "Comments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FeedbackId",
                schema: "FeedbackModule",
                table: "Comments",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_RoomId",
                schema: "FeedbackModule",
                table: "Feedbacks",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUserIdRoom_RoomId",
                schema: "FeedbackModule",
                table: "NotificationUserIdRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_FeedbackId",
                schema: "FeedbackModule",
                table: "Votes",
                column: "FeedbackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassIdRoom",
                schema: "FeedbackModule");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "FeedbackModule");

            migrationBuilder.DropTable(
                name: "NotificationUserIdRoom",
                schema: "FeedbackModule");

            migrationBuilder.DropTable(
                name: "Votes",
                schema: "FeedbackModule");

            migrationBuilder.DropTable(
                name: "ClassId",
                schema: "FeedbackModule");

            migrationBuilder.DropTable(
                name: "NotificationUserId",
                schema: "FeedbackModule");

            migrationBuilder.DropTable(
                name: "Feedbacks",
                schema: "FeedbackModule");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "FeedbackModule");
        }
    }
}
