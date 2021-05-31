using System;
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
                name: "Education_cycles_and_modules",
                columns: table => new
                {
                    Education_cycle_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Education_cycle_index = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Education_cycle_parent_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_cycles_and_modules", x => x.Education_cycle_id);
                    table.ForeignKey(
                        name: "FK_Education_cycles_and_modules_Education_cycles_and_modules",
                        column: x => x.Education_cycle_parent_id,
                        principalTable: "Education_cycles_and_modules",
                        principalColumn: "Education_cycle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Education_forms",
                columns: table => new
                {
                    Education_form_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_forms", x => x.Education_form_id);
                });

            migrationBuilder.CreateTable(
                name: "Education_levels",
                columns: table => new
                {
                    Education_level_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_levels", x => x.Education_level_id);
                });

            migrationBuilder.CreateTable(
                name: "Intermediate_certification_forms",
                columns: table => new
                {
                    Certification_form_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intermediate_certification_forms", x => x.Certification_form_id);
                });

            migrationBuilder.CreateTable(
                name: "Lesson_types",
                columns: table => new
                {
                    Lesson_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson_types", x => x.Lesson_type_id);
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
                    Number = table.Column<byte>(type: "tinyint", nullable: false),
                    Weeks_count = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Semester_id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Specialtie_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specialtie_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Implemented_specialty_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Specialtie_id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Discipline_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discipline_index = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Cathedra_id = table.Column<int>(type: "int", nullable: true),
                    Education_cycle_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Discipline_id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Cathedras",
                        column: x => x.Cathedra_id,
                        principalTable: "Cathedras",
                        principalColumn: "Cathedra_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplines_Education_cycles_and_modules",
                        column: x => x.Education_cycle_id,
                        principalTable: "Education_cycles_and_modules",
                        principalColumn: "Education_cycle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Received_education_forms",
                columns: table => new
                {
                    Received_education_form_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Education_form_id = table.Column<int>(type: "int", nullable: false),
                    Additional_info = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Received_education_forms", x => x.Received_education_form_id);
                    table.ForeignKey(
                        name: "FK_Received_education_forms_Education_forms",
                        column: x => x.Education_form_id,
                        principalTable: "Education_forms",
                        principalColumn: "Education_form_id",
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
                name: "Cathedra_specialties",
                columns: table => new
                {
                    Cathedra_id = table.Column<int>(type: "int", nullable: false),
                    Specialtie_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedra_specialties", x => new { x.Cathedra_id, x.Specialtie_id });
                    table.ForeignKey(
                        name: "FK_Cathedra_specialties_Cathedras",
                        column: x => x.Cathedra_id,
                        principalTable: "Cathedras",
                        principalColumn: "Cathedra_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cathedra_specialties_Specialties",
                        column: x => x.Specialtie_id,
                        principalTable: "Specialties",
                        principalColumn: "Specialtie_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Education_plans",
                columns: table => new
                {
                    Education_plan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specialtie_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    Academic_year_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_plans", x => x.Education_plan_id);
                    table.ForeignKey(
                        name: "FK_Education_plans_Academic_years",
                        column: x => x.Academic_year_id,
                        principalTable: "Academic_years",
                        principalColumn: "Academic_year_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_plans_Specialties",
                        column: x => x.Specialtie_id,
                        principalTable: "Specialties",
                        principalColumn: "Specialtie_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Received_specialties",
                columns: table => new
                {
                    Received_specialty_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specialtie_id = table.Column<int>(type: "int", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Received_specialties", x => x.Received_specialty_id);
                    table.ForeignKey(
                        name: "FK_Received_specialties_Specialties",
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
                    Theory_lesson_hours = table.Column<short>(type: "smallint", nullable: false),
                    Practice_work_hours = table.Column<short>(type: "smallint", nullable: false),
                    Laboratory_work_hours = table.Column<short>(type: "smallint", nullable: false),
                    Control_work_hours = table.Column<short>(type: "smallint", nullable: false),
                    Independent_work_hours = table.Column<short>(type: "smallint", nullable: false),
                    Consultation_hours = table.Column<short>(type: "smallint", nullable: false),
                    Exam_hours = table.Column<short>(type: "smallint", nullable: false),
                    Educational_practice_hours = table.Column<short>(type: "smallint", nullable: false),
                    Production_practice_hours = table.Column<short>(type: "smallint", nullable: false),
                    Certification_form_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester_disciplines", x => x.Semester_discipline_id);
                    table.ForeignKey(
                        name: "FK_Semester_disciplines_Intermediate_certification_forms",
                        column: x => x.Certification_form_id,
                        principalTable: "Intermediate_certification_forms",
                        principalColumn: "Certification_form_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Semesters_Disciplines_Disciplines",
                        column: x => x.Discipline_id,
                        principalTable: "Disciplines",
                        principalColumn: "Discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Semesters_Disciplines_Semesters",
                        column: x => x.Semester_id,
                        principalTable: "Semesters",
                        principalColumn: "Semester_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
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
                    table.PrimaryKey("PK_Accounts", x => x.Account_id);
                    table.ForeignKey(
                        name: "FK_Account_Employees",
                        column: x => x.Employee_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Audiences",
                columns: table => new
                {
                    Audience_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Employee_head_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audiences", x => x.Audience_id);
                    table.ForeignKey(
                        name: "FK_Audiences_Employees",
                        column: x => x.Employee_head_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee_cathedras",
                columns: table => new
                {
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Cathedra_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_cathedras", x => new { x.Employee_id, x.Cathedra_id });
                    table.ForeignKey(
                        name: "FK_Employee_cathedras_Cathedras",
                        column: x => x.Cathedra_id,
                        principalTable: "Cathedras",
                        principalColumn: "Cathedra_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_cathedras_Employees",
                        column: x => x.Employee_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Received_educations",
                columns: table => new
                {
                    Received_education_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Received_specialty_id = table.Column<int>(type: "int", nullable: false),
                    Received_education_form_id = table.Column<int>(type: "int", nullable: false),
                    Education_level_id = table.Column<int>(type: "int", nullable: false),
                    Study_period_months = table.Column<short>(type: "smallint", nullable: false),
                    Is_budget = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Received_educations", x => x.Received_education_id);
                    table.ForeignKey(
                        name: "FK_Received_educations_Education_levels",
                        column: x => x.Education_level_id,
                        principalTable: "Education_levels",
                        principalColumn: "Education_level_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Received_educations_Received_education_forms",
                        column: x => x.Received_education_form_id,
                        principalTable: "Received_education_forms",
                        principalColumn: "Received_education_form_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Received_educations_Received_specialties",
                        column: x => x.Received_specialty_id,
                        principalTable: "Received_specialties",
                        principalColumn: "Received_specialty_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Education_plan_semester_disciplines",
                columns: table => new
                {
                    Education_plan_id = table.Column<int>(type: "int", nullable: false),
                    Semester_discipline_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_plan_semester_disciplines", x => new { x.Education_plan_id, x.Semester_discipline_id });
                    table.ForeignKey(
                        name: "FK_Education_plan_semester_disciplines_Education_plans",
                        column: x => x.Education_plan_id,
                        principalTable: "Education_plans",
                        principalColumn: "Education_plan_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_plan_semester_disciplines_Semester_disciplines",
                        column: x => x.Semester_discipline_id,
                        principalTable: "Semester_disciplines",
                        principalColumn: "Semester_discipline_id",
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
                    Received_education_id = table.Column<int>(type: "int", nullable: false),
                    Education_plan_id = table.Column<int>(type: "int", nullable: true),
                    Receipt_year = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Group_id);
                    table.ForeignKey(
                        name: "FK_Groups_Education_plans",
                        column: x => x.Education_plan_id,
                        principalTable: "Education_plans",
                        principalColumn: "Education_plan_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_Employees",
                        column: x => x.Curator_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Groups_Received_educations",
                        column: x => x.Received_education_id,
                        principalTable: "Received_educations",
                        principalColumn: "Received_education_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fixed_disciplines",
                columns: table => new
                {
                    Fixed_discipline_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Semester_discipline_id = table.Column<int>(type: "int", nullable: false),
                    Group_id = table.Column<int>(type: "int", nullable: false),
                    Is_agreed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixed_disciplines", x => x.Fixed_discipline_id);
                    table.ForeignKey(
                        name: "FK_Fixed_disciplines_Employees",
                        column: x => x.Employee_id,
                        principalTable: "Employees",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixed_disciplines_Groups",
                        column: x => x.Group_id,
                        principalTable: "Groups",
                        principalColumn: "Group_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixed_disciplines_Semester_disciplines",
                        column: x => x.Semester_discipline_id,
                        principalTable: "Semester_disciplines",
                        principalColumn: "Semester_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule_disciplines",
                columns: table => new
                {
                    Schedule_discipline_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fixed_discipline_id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Pair_number = table.Column<int>(type: "int", nullable: false),
                    Audience_id = table.Column<int>(type: "int", nullable: true),
                    Is_even_pair = table.Column<bool>(type: "bit", nullable: true),
                    Is_first_subgroup = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule_disciplines", x => x.Schedule_discipline_id);
                    table.ForeignKey(
                        name: "FK_Schedule_disciplines_Audiences",
                        column: x => x.Audience_id,
                        principalTable: "Audiences",
                        principalColumn: "Audience_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_disciplines_Fixed_disciplines",
                        column: x => x.Fixed_discipline_id,
                        principalTable: "Fixed_disciplines",
                        principalColumn: "Fixed_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule_discipline_replacement",
                columns: table => new
                {
                    Schedule_discipline_replacement_id = table.Column<int>(type: "int", nullable: false),
                    Schedule_discipline_id = table.Column<int>(type: "int", nullable: true),
                    Fixed_discipline_id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Pair_number = table.Column<int>(type: "int", nullable: false),
                    Audience_id = table.Column<int>(type: "int", nullable: true),
                    Is_first_subgroup = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule_discipline_replacement", x => x.Schedule_discipline_replacement_id);
                    table.ForeignKey(
                        name: "FK_Schedule_discipline_replacement_Audiences",
                        column: x => x.Audience_id,
                        principalTable: "Audiences",
                        principalColumn: "Audience_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_discipline_replacement_Fixed_disciplines",
                        column: x => x.Fixed_discipline_id,
                        principalTable: "Fixed_disciplines",
                        principalColumn: "Fixed_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedule_discipline_replacement_Schedule_disciplines",
                        column: x => x.Schedule_discipline_id,
                        principalTable: "Schedule_disciplines",
                        principalColumn: "Schedule_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conducted_pairs",
                columns: table => new
                {
                    Conducted_pair_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Schedule_discipline_id = table.Column<int>(type: "int", nullable: true),
                    Schedule_discipline_replacement_id = table.Column<int>(type: "int", nullable: true),
                    Lesson_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conducted_pairs", x => x.Conducted_pair_id);
                    table.ForeignKey(
                        name: "FK_Conducted_pairs_Lesson_types",
                        column: x => x.Lesson_type_id,
                        principalTable: "Lesson_types",
                        principalColumn: "Lesson_type_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conducted_pairs_Schedule_discipline_replacement",
                        column: x => x.Schedule_discipline_replacement_id,
                        principalTable: "Schedule_discipline_replacement",
                        principalColumn: "Schedule_discipline_replacement_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conducted_pairs_Schedule_disciplines1",
                        column: x => x.Schedule_discipline_id,
                        principalTable: "Schedule_disciplines",
                        principalColumn: "Schedule_discipline_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Employee_id",
                table: "Accounts",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Audiences_Employee_head_id",
                table: "Audiences",
                column: "Employee_head_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cathedra_specialties_Specialtie_id",
                table: "Cathedra_specialties",
                column: "Specialtie_id");

            migrationBuilder.CreateIndex(
                name: "IX_Conducted_pairs_Lesson_type_id",
                table: "Conducted_pairs",
                column: "Lesson_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Conducted_pairs_Schedule_discipline_id",
                table: "Conducted_pairs",
                column: "Schedule_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Conducted_pairs_Schedule_discipline_replacement_id",
                table: "Conducted_pairs",
                column: "Schedule_discipline_replacement_id");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Cathedra_id",
                table: "Disciplines",
                column: "Cathedra_id");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Education_cycle_id",
                table: "Disciplines",
                column: "Education_cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_Education_cycles_and_modules_Education_cycle_parent_id",
                table: "Education_cycles_and_modules",
                column: "Education_cycle_parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Education_plan_semester_disciplines_Semester_discipline_id",
                table: "Education_plan_semester_disciplines",
                column: "Semester_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Education_plans_Academic_year_id",
                table: "Education_plans",
                column: "Academic_year_id");

            migrationBuilder.CreateIndex(
                name: "IX_Education_plans_Specialtie_id",
                table: "Education_plans",
                column: "Specialtie_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_cathedras_Cathedra_id",
                table: "Employee_cathedras",
                column: "Cathedra_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Post_id",
                table: "Employees",
                column: "Post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixed_disciplines_Employee_id",
                table: "Fixed_disciplines",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixed_disciplines_Group_id",
                table: "Fixed_disciplines",
                column: "Group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixed_disciplines_Semester_discipline_id",
                table: "Fixed_disciplines",
                column: "Semester_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Curator_id",
                table: "Groups",
                column: "Curator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Education_plan_id",
                table: "Groups",
                column: "Education_plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Received_education_id",
                table: "Groups",
                column: "Received_education_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_education_forms_Education_form_id",
                table: "Received_education_forms",
                column: "Education_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_educations_Education_level_id",
                table: "Received_educations",
                column: "Education_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_educations_Received_education_form_id",
                table: "Received_educations",
                column: "Received_education_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_educations_Received_specialty_id",
                table: "Received_educations",
                column: "Received_specialty_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_specialties_Specialtie_id",
                table: "Received_specialties",
                column: "Specialtie_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_discipline_replacement_Audience_id",
                table: "Schedule_discipline_replacement",
                column: "Audience_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_discipline_replacement_Fixed_discipline_id",
                table: "Schedule_discipline_replacement",
                column: "Fixed_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_discipline_replacement_Schedule_discipline_id",
                table: "Schedule_discipline_replacement",
                column: "Schedule_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_disciplines_Audience_id",
                table: "Schedule_disciplines",
                column: "Audience_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_disciplines_Fixed_discipline_id",
                table: "Schedule_disciplines",
                column: "Fixed_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_disciplines_Certification_form_id",
                table: "Semester_disciplines",
                column: "Certification_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_disciplines_Discipline_id",
                table: "Semester_disciplines",
                column: "Discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_disciplines_Semester_id",
                table: "Semester_disciplines",
                column: "Semester_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Cathedra_specialties");

            migrationBuilder.DropTable(
                name: "Conducted_pairs");

            migrationBuilder.DropTable(
                name: "Education_plan_semester_disciplines");

            migrationBuilder.DropTable(
                name: "Employee_cathedras");

            migrationBuilder.DropTable(
                name: "Lesson_types");

            migrationBuilder.DropTable(
                name: "Schedule_discipline_replacement");

            migrationBuilder.DropTable(
                name: "Schedule_disciplines");

            migrationBuilder.DropTable(
                name: "Audiences");

            migrationBuilder.DropTable(
                name: "Fixed_disciplines");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Semester_disciplines");

            migrationBuilder.DropTable(
                name: "Education_plans");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Received_educations");

            migrationBuilder.DropTable(
                name: "Intermediate_certification_forms");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Academic_years");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Education_levels");

            migrationBuilder.DropTable(
                name: "Received_education_forms");

            migrationBuilder.DropTable(
                name: "Received_specialties");

            migrationBuilder.DropTable(
                name: "Cathedras");

            migrationBuilder.DropTable(
                name: "Education_cycles_and_modules");

            migrationBuilder.DropTable(
                name: "Education_forms");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
