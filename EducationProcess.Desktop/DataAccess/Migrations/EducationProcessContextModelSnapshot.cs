﻿// <auto-generated />
using EducationProcess.Desktop.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EducationProcess.Desktop.DataAccess.Migrations
{
    [DbContext(typeof(EducationProcessContext))]
    partial class EducationProcessContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.AcademicYears", b =>
                {
                    b.Property<int>("AcademicYearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Academic_year_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("BeginingYear")
                        .HasColumnType("smallint")
                        .HasColumnName("Begining_year");

                    b.Property<short>("EndingYear")
                        .HasColumnType("smallint")
                        .HasColumnName("Ending_year");

                    b.HasKey("AcademicYearId");

                    b.ToTable("Academic_years");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Account_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("Employee_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("AccountId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Cathedras", b =>
                {
                    b.Property<int>("CathedraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Cathedra_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("CathedraId");

                    b.ToTable("Cathedras");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Disciplines", b =>
                {
                    b.Property<int>("DisciplineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Discipline_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("ConsultationHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Consultation_hours");

                    b.Property<short>("ControlWorkVerificationHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Control_work_verification_hours");

                    b.Property<short>("CourseworkProjectHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Coursework_project_hours");

                    b.Property<short>("DiplomaProjectHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Diploma_project_hours");

                    b.Property<short>("ExamHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Exam_hours");

                    b.Property<short>("LaboratoryLessonHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Laboratory_lesson_hours");

                    b.Property<short>("LectionLessonHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Lection_lesson_hours");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(125)
                        .HasColumnType("nvarchar(125)");

                    b.Property<string>("Note")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<short>("PracticalLessonHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Practical_lesson_hours");

                    b.Property<short>("PracticeHeadHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Practice_head_hours");

                    b.Property<short>("SecHours")
                        .HasColumnType("smallint")
                        .HasColumnName("SEC_hours");

                    b.Property<short>("TestHours")
                        .HasColumnType("smallint")
                        .HasColumnName("Test_hours");

                    b.HasKey("DisciplineId");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Employees", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Employee_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Middlename")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("Post_id");

                    b.HasKey("EmployeeId");

                    b.HasIndex("PostId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Groups", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Group_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("CourseNumber")
                        .HasColumnType("tinyint")
                        .HasColumnName("Course_number");

                    b.Property<int>("CuratorId")
                        .HasColumnType("int")
                        .HasColumnName("Curator_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<short>("ReceiptYear")
                        .HasColumnType("smallint")
                        .HasColumnName("Receipt_year");

                    b.Property<int>("SpecialtieId")
                        .HasColumnType("int")
                        .HasColumnName("Specialtie_id");

                    b.HasKey("GroupId");

                    b.HasIndex("CuratorId");

                    b.HasIndex("SpecialtieId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Posts", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Post_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("PostId")
                        .HasName("PK_Speciality");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.SemesterDisciplines", b =>
                {
                    b.Property<int>("SemesterDisciplineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Semester_discipline_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int")
                        .HasColumnName("Discipline_id");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("Employee_id");

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("Group_id");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Group_name");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit")
                        .HasColumnName("Is_accepted");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int")
                        .HasColumnName("Semester_id");

                    b.HasKey("SemesterDisciplineId")
                        .HasName("PK_Semesters_Disciplines");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SemesterId");

                    b.ToTable("Semester_disciplines");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Semesters", b =>
                {
                    b.Property<int>("SemesterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Semester_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AcademicYearId")
                        .HasColumnType("int")
                        .HasColumnName("Academic_year_id");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("SemesterId");

                    b.HasIndex("AcademicYearId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Specialties", b =>
                {
                    b.Property<int>("SpecialtieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Specialtie_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("CathedraId")
                        .HasColumnType("int")
                        .HasColumnName("Cathedra_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("SpecialtieId");

                    b.HasIndex("CathedraId");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Account", b =>
                {
                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Employees", "Employee")
                        .WithMany("Account")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_Account_Employees")
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Employees", b =>
                {
                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Posts", "Post")
                        .WithMany("Employees")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_Employees_Posts")
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Groups", b =>
                {
                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Employees", "Curator")
                        .WithMany("Groups")
                        .HasForeignKey("CuratorId")
                        .HasConstraintName("FK_Groups_Employees")
                        .IsRequired();

                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Specialties", "Specialtie")
                        .WithMany("Groups")
                        .HasForeignKey("SpecialtieId")
                        .HasConstraintName("FK_Groups_Specialties")
                        .IsRequired();

                    b.Navigation("Curator");

                    b.Navigation("Specialtie");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.SemesterDisciplines", b =>
                {
                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Disciplines", "Discipline")
                        .WithMany("SemesterDisciplines")
                        .HasForeignKey("DisciplineId")
                        .HasConstraintName("FK_Semesters_Disciplines_Disciplines")
                        .IsRequired();

                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Employees", "Employee")
                        .WithMany("SemesterDisciplines")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_Semesters_Disciplines_Employees")
                        .IsRequired();

                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Groups", "Group")
                        .WithMany("SemesterDisciplines")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_Semesters_Disciplines_Groups")
                        .IsRequired();

                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Semesters", "Semester")
                        .WithMany("SemesterDisciplines")
                        .HasForeignKey("SemesterId")
                        .HasConstraintName("FK_Semesters_Disciplines_Semesters")
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Employee");

                    b.Navigation("Group");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Semesters", b =>
                {
                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.AcademicYears", "AcademicYear")
                        .WithMany("Semesters")
                        .HasForeignKey("AcademicYearId")
                        .HasConstraintName("FK_Semesters_Academic_years")
                        .IsRequired();

                    b.Navigation("AcademicYear");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Specialties", b =>
                {
                    b.HasOne("EducationProcess.Desktop.DataAccess.Entities.Cathedras", "Cathedra")
                        .WithMany("Specialties")
                        .HasForeignKey("CathedraId")
                        .HasConstraintName("FK_Specialties_Cathedras")
                        .IsRequired();

                    b.Navigation("Cathedra");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.AcademicYears", b =>
                {
                    b.Navigation("Semesters");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Cathedras", b =>
                {
                    b.Navigation("Specialties");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Disciplines", b =>
                {
                    b.Navigation("SemesterDisciplines");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Employees", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Groups");

                    b.Navigation("SemesterDisciplines");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Groups", b =>
                {
                    b.Navigation("SemesterDisciplines");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Posts", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Semesters", b =>
                {
                    b.Navigation("SemesterDisciplines");
                });

            modelBuilder.Entity("EducationProcess.Desktop.DataAccess.Entities.Specialties", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
