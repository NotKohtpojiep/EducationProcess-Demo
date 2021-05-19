using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationProcess.Desktop.DataAccess.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Academic_years",
                columns: table => new
                {
                    Academic_year_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Begining_year = table.Column<short>(type: "smallint", nullable: false),
                    Ending_year = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academic_years", x => x.Academic_year_id);
                });

            migrationBuilder.CreateTable(
                name: "Cathedras",
                columns: table => new
                {
                    Cathedra_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedras", x => x.Cathedra_id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Discipline_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discipline_index = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Lection_lesson_hours = table.Column<short>(type: "smallint", nullable: false),
                    Practical_lesson_hours = table.Column<short>(type: "smallint", nullable: false),
                    Laboratory_lesson_hours = table.Column<short>(type: "smallint", nullable: false),
                    Consultation_hours = table.Column<short>(type: "smallint", nullable: false),
                    Test_hours = table.Column<short>(type: "smallint", nullable: false),
                    Exam_hours = table.Column<short>(type: "smallint", nullable: false),
                    Coursework_project_hours = table.Column<short>(type: "smallint", nullable: false),
                    Diploma_project_hours = table.Column<short>(type: "smallint", nullable: false),
                    SEC_hours = table.Column<short>(type: "smallint", nullable: false),
                    Practice_head_hours = table.Column<short>(type: "smallint", nullable: false),
                    Control_work_verification_hours = table.Column<short>(type: "smallint", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Post_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Post_id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Semester_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Academic_year_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Semester_id);
                    table.ForeignKey(
                        name: "FK_Semesters_Academic_years",
                        column: x => x.Academic_year_id,
                        principalTable: "Academic_years",
                        principalColumn: "Academic_year_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Specialtie_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cathedra_id = table.Column<int>(type: "int", nullable: false),
                    Specialtie_index = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Specialtie_id);
                    table.ForeignKey(
                        name: "FK_Specialties_Cathedras",
                        column: x => x.Cathedra_id,
                        principalTable: "Cathedras",
                        principalColumn: "Cathedra_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Employee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    Post_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Employee_id);
                    table.ForeignKey(
                        name: "FK_Employees_Posts",
                        column: x => x.Post_id,
                        principalTable: "Posts",
                        principalColumn: "Post_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Account_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Account_id);
                    table.ForeignKey(
                        name: "FK_Account_Employees",
                        column: x => x.Employee_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Course_number = table.Column<byte>(type: "tinyint", nullable: false),
                    Curator_id = table.Column<int>(type: "int", nullable: false),
                    Specialtie_id = table.Column<int>(type: "int", nullable: false),
                    Receipt_year = table.Column<short>(type: "smallint", nullable: false),
                    IsBudget = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Group_id);
                    table.ForeignKey(
                        name: "FK_Groups_Employees",
                        column: x => x.Curator_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_Specialties",
                        column: x => x.Specialtie_id,
                        principalTable: "Specialties",
                        principalColumn: "Specialtie_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Semester_disciplines",
                columns: table => new
                {
                    Semester_discipline_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semester_id = table.Column<int>(type: "int", nullable: false),
                    Discipline_id = table.Column<int>(type: "int", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Group_id = table.Column<int>(type: "int", nullable: false),
                    Is_accepted = table.Column<bool>(type: "bit", nullable: false),
                    Group_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester_disciplines", x => x.Semester_discipline_id);
                    table.ForeignKey(
                        name: "FK_Semesters_Disciplines_Disciplines",
                        column: x => x.Discipline_id,
                        principalTable: "Disciplines",
                        principalColumn: "Discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Semesters_Disciplines_Employees",
                        column: x => x.Employee_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Semesters_Disciplines_Groups",
                        column: x => x.Group_id,
                        principalTable: "Groups",
                        principalColumn: "Group_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Semesters_Disciplines_Semesters",
                        column: x => x.Semester_id,
                        principalTable: "Semesters",
                        principalColumn: "Semester_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Employee_id",
                table: "Account",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Post_id",
                table: "Employees",
                column: "Post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Curator_id",
                table: "Groups",
                column: "Curator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Specialtie_id",
                table: "Groups",
                column: "Specialtie_id");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_disciplines_Discipline_id",
                table: "Semester_disciplines",
                column: "Discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_disciplines_Employee_id",
                table: "Semester_disciplines",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_disciplines_Group_id",
                table: "Semester_disciplines",
                column: "Group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_disciplines_Semester_id",
                table: "Semester_disciplines",
                column: "Semester_id");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_Academic_year_id",
                table: "Semesters",
                column: "Academic_year_id");

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_Cathedra_id",
                table: "Specialties",
                column: "Cathedra_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Semester_disciplines");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Academic_years");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Cathedras");
        }
    }
}
