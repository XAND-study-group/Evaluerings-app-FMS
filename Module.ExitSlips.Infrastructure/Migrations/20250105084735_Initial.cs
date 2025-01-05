using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.ExitSlip.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ExitSlipModule");

            migrationBuilder.CreateTable(
                name: "ExitSlips",
                schema: "ExitSlipModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false),
                    MaxQuestionCount_Value = table.Column<int>(type: "int", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExitSlips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "ExitSlipModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExitSlipId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Text_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_ExitSlips_ExitSlipId",
                        column: x => x.ExitSlipId,
                        principalSchema: "ExitSlipModule",
                        principalTable: "ExitSlips",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                schema: "ExitSlipModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Text_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "ExitSlipModule",
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                schema: "ExitSlipModule",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExitSlipId",
                schema: "ExitSlipModule",
                table: "Questions",
                column: "ExitSlipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers",
                schema: "ExitSlipModule");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "ExitSlipModule");

            migrationBuilder.DropTable(
                name: "ExitSlips",
                schema: "ExitSlipModule");
        }
    }
}
