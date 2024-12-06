using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    School = table.Column<int>(type: "int", nullable: false),
                    EducationRange_End = table.Column<DateOnly>(type: "date", nullable: false),
                    EducationRange_Start = table.Column<DateOnly>(type: "date", nullable: false),
                    Name_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemesterNumber_Value = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentCapacity_Value = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClassRoom_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LectureDate_Value = table.Column<DateOnly>(type: "date", nullable: false),
                    TimePeriod_Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimePeriod_From = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimePeriod_To = table.Column<TimeOnly>(type: "time", nullable: false),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    ClassIdStudent = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClassIdTeacher = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Firstname_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Classes_ClassIdStudent",
                        column: x => x.ClassIdStudent,
                        principalTable: "Classes",
                        principalColumn: "RoomId");
                    table.ForeignKey(
                        name: "FK_Users_Classes_ClassIdTeacher",
                        column: x => x.ClassIdTeacher,
                        principalTable: "Classes",
                        principalColumn: "RoomId");
                    table.ForeignKey(
                        name: "FK_Users_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "AccountClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterUser",
                columns: table => new
                {
                    SemesterResponsiblesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemestersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterUser", x => new { x.SemesterResponsiblesId, x.SemestersId });
                    table.ForeignKey(
                        name: "FK_SemesterUser_Semesters_SemestersId",
                        column: x => x.SemestersId,
                        principalTable: "Semesters",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterUser_Users_SemesterResponsiblesId",
                        column: x => x.SemesterResponsiblesId,
                        principalTable: "Users",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountClaims_UserId",
                table: "AccountClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SemesterId",
                table: "Classes",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_SubjectId",
                table: "Lectures",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterUser_SemestersId",
                table: "SemesterUser",
                column: "SemestersId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ClassId",
                table: "Subjects",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassIdStudent",
                table: "Users",
                column: "ClassIdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassIdTeacher",
                table: "Users",
                column: "ClassIdTeacher");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LectureId",
                table: "Users",
                column: "LectureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountClaims");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "SemesterUser");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Semesters");
        }
    }
}
