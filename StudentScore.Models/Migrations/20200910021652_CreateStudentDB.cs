using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentScore.Models.Migrations
{
    public partial class CreateStudentDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportCard",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    IsRemove = table.Column<bool>(nullable: false),
                    Chinese = table.Column<int>(nullable: false),
                    Math = table.Column<int>(nullable: false),
                    English = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCard", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AllStudentClass",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    IsRemove = table.Column<bool>(nullable: false),
                    Grades = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClass", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StudentInfo",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    IsRemove = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    StudentNumber = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    ReportCardID = table.Column<long>(nullable: false),
                    StudentClassID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentInfo_ReportCard_ReportCardID",
                        column: x => x.ReportCardID,
                        principalTable: "ReportCard",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentInfo_StudentClass_StudentClassID",
                        column: x => x.StudentClassID,
                        principalTable: "AllStudentClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfo_ReportCardID",
                table: "StudentInfo",
                column: "ReportCardID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInfo_StudentClassID",
                table: "StudentInfo",
                column: "StudentClassID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentInfo");

            migrationBuilder.DropTable(
                name: "ReportCard");

            migrationBuilder.DropTable(
                name: "AllStudentClass");
        }
    }
}
