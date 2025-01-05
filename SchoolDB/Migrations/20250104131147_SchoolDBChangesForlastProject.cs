using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDB.Migrations
{
    /// <inheritdoc />
    public partial class SchoolDBChangesForlastProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Course_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courses__37E005DBA76AC388", x => x.Course_Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Employee_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__781134A16B6D0D89", x => x.Employee_Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Student_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Students__A2F4E98C5A1D3821", x => x.Student_Id);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Assignment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_employee_id = table.Column<int>(type: "int", nullable: true),
                    Fk_course_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assignme__9E0E9F2FFB1D9FF6", x => x.Assignment_Id);
                    table.ForeignKey(
                        name: "FK__Assignmen__Fk_co__47DBAE45",
                        column: x => x.Fk_course_id,
                        principalTable: "Courses",
                        principalColumn: "Course_Id");
                    table.ForeignKey(
                        name: "FK__Assignmen__Fk_em__46E78A0C",
                        column: x => x.Fk_employee_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_Id");
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Enrollment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_StudentID = table.Column<int>(type: "int", nullable: true),
                    Fk_CourseID = table.Column<int>(type: "int", nullable: true),
                    EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Enrollme__4365BD4A8F29B8DC", x => x.Enrollment_Id);
                    table.ForeignKey(
                        name: "FK__Enrollmen__Enrol__4316F928",
                        column: x => x.Fk_StudentID,
                        principalTable: "Students",
                        principalColumn: "Student_Id");
                    table.ForeignKey(
                        name: "FK__Enrollmen__Fk_Co__440B1D61",
                        column: x => x.Fk_CourseID,
                        principalTable: "Courses",
                        principalColumn: "Course_Id");
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Grade_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_StudentID = table.Column<int>(type: "int", nullable: true),
                    FK_CourseID = table.Column<int>(type: "int", nullable: true),
                    FK_EmployeeID = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(18,2)", unicode: false, maxLength: 5, nullable: true),
                    GradeDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Grades__D4437133DF502732", x => x.Grade_Id);
                    table.ForeignKey(
                        name: "FK__Grades__FK_Cours__3F466844",
                        column: x => x.FK_CourseID,
                        principalTable: "Courses",
                        principalColumn: "Course_Id");
                    table.ForeignKey(
                        name: "FK__Grades__FK_Emplo__403A8C7D",
                        column: x => x.FK_EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Employee_Id");
                    table.ForeignKey(
                        name: "FK__Grades__GradeDat__3E52440B",
                        column: x => x.FK_StudentID,
                        principalTable: "Students",
                        principalColumn: "Student_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Fk_course_id",
                table: "Assignments",
                column: "Fk_course_id");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Fk_employee_id",
                table: "Assignments",
                column: "Fk_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_Fk_CourseID",
                table: "Enrollments",
                column: "Fk_CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_Fk_StudentID",
                table: "Enrollments",
                column: "Fk_StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_CourseID",
                table: "Grades",
                column: "FK_CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_EmployeeID",
                table: "Grades",
                column: "FK_EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_FK_StudentID",
                table: "Grades",
                column: "FK_StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
