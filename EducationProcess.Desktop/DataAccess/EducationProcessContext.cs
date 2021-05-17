using System;
using EducationProcess.Desktop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EducationProcess.Desktop.DataAccess
{
    public partial class EducationProcessContext : DbContext
    {
        public EducationProcessContext()
        {
        }

        public EducationProcessContext(DbContextOptions<EducationProcessContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Cathedra> Cathedras { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<SemesterDiscipline> SemesterDisciplines { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-F79I7DI\\PLEASEBEMYSEMPAI;Database=EducationProcess;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.ToTable("Academic_years");

                entity.Property(e => e.AcademicYearId)
                    .ValueGeneratedNever()
                    .HasColumnName("Academic_year_id");

                entity.Property(e => e.BeginingYear).HasColumnName("Begining_year");

                entity.Property(e => e.EndingYear).HasColumnName("Ending_year");
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("Account_id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Employees");
            });

            modelBuilder.Entity<Cathedra>(entity =>
            {
                entity.Property(e => e.CathedraId).HasColumnName("Cathedra_id");

                entity.Property(e => e.Abbreviation).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.Property(e => e.DisciplineId).HasColumnName("Discipline_id");

                entity.Property(e => e.ConsultationHours).HasColumnName("Consultation_hours");

                entity.Property(e => e.ControlWorkVerificationHours).HasColumnName("Control_work_verification_hours");

                entity.Property(e => e.CourseworkProjectHours).HasColumnName("Coursework_project_hours");

                entity.Property(e => e.DiplomaProjectHours).HasColumnName("Diploma_project_hours");

                entity.Property(e => e.ExamHours).HasColumnName("Exam_hours");

                entity.Property(e => e.LaboratoryLessonHours).HasColumnName("Laboratory_lesson_hours");

                entity.Property(e => e.LectionLessonHours).HasColumnName("Lection_lesson_hours");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(125);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.PracticalLessonHours).HasColumnName("Practical_lesson_hours");

                entity.Property(e => e.PracticeHeadHours).HasColumnName("Practice_head_hours");

                entity.Property(e => e.SecHours).HasColumnName("SEC_hours");

                entity.Property(e => e.TestHours).HasColumnName("Test_hours");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.Property(e => e.Middlename).HasMaxLength(75);

                entity.Property(e => e.PostId).HasColumnName("Post_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Posts");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId).HasColumnName("Group_id");

                entity.Property(e => e.CourseNumber).HasColumnName("Course_number");

                entity.Property(e => e.CuratorId).HasColumnName("Curator_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ReceiptYear).HasColumnName("Receipt_year");

                entity.Property(e => e.SpecialtieId).HasColumnName("Specialtie_id");

                entity.HasOne(d => d.Curator)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CuratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_Employees");

                entity.HasOne(d => d.Specialtie)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.SpecialtieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_Specialties");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId).HasColumnName("Post_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.Property(e => e.SemesterId).HasColumnName("Semester_id");

                entity.Property(e => e.AcademicYearId).HasColumnName("Academic_year_id");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Semesters)
                    .HasForeignKey(d => d.AcademicYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semesters_Academic_years");
            });

            modelBuilder.Entity<SemesterDiscipline>(entity =>
            {
                entity.ToTable("Semester_disciplines");

                entity.Property(e => e.SemesterDisciplineId).HasColumnName("Semester_discipline_id");

                entity.Property(e => e.DisciplineId).HasColumnName("Discipline_id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.GroupId).HasColumnName("Group_id");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Group_name");

                entity.Property(e => e.IsAccepted).HasColumnName("Is_accepted");

                entity.Property(e => e.SemesterId).HasColumnName("Semester_id");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.SemesterDisciplines)
                    .HasForeignKey(d => d.DisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semesters_Disciplines_Disciplines");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SemesterDisciplines)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semesters_Disciplines_Employees");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.SemesterDisciplines)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semesters_Disciplines_Groups");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.SemesterDisciplines)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semesters_Disciplines_Semesters");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasKey(e => e.SpecialtieId);

                entity.Property(e => e.SpecialtieId)
                    .ValueGeneratedNever()
                    .HasColumnName("Specialtie_id");

                entity.Property(e => e.Abbreviation).HasMaxLength(10);

                entity.Property(e => e.CathedraId).HasColumnName("Cathedra_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75);

                entity.HasOne(d => d.Cathedra)
                    .WithMany(p => p.Specialties)
                    .HasForeignKey(d => d.CathedraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Specialties_Cathedras");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
