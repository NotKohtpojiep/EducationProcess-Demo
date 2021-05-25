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
        public virtual DbSet<Audience> Audiences { get; set; }
        public virtual DbSet<Cathedra> Cathedras { get; set; }
        public virtual DbSet<CathedraSpecialty> CathedraSpecialties { get; set; }
        public virtual DbSet<ConductedPair> ConductedPairs { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<EducationCyclesAndModule> EducationCyclesAndModules { get; set; }
        public virtual DbSet<EducationForm> EducationForms { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<EducationPlan> EducationPlans { get; set; }
        public virtual DbSet<EducationPlanSemesterDiscipline> EducationPlanSemesterDisciplines { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<FixedDiscipline> FixedDisciplines { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<IntermediateCertificationForm> IntermediateCertificationForms { get; set; }
        public virtual DbSet<LessonType> LessonTypes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<ReceivedEducation> ReceivedEducations { get; set; }
        public virtual DbSet<ReceivedEducationForm> ReceivedEducationForms { get; set; }
        public virtual DbSet<ReceivedSpecialty> ReceivedSpecialties { get; set; }
        public virtual DbSet<ScheduleDiscipline> ScheduleDisciplines { get; set; }
        public virtual DbSet<ScheduleDisciplineReplacement> ScheduleDisciplineReplacements { get; set; }
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

                entity.Property(e => e.AcademicYearId).HasColumnName("Academic_year_id");

                entity.Property(e => e.BeginingYear).HasColumnName("Begining_year");

                entity.Property(e => e.EndingYear).HasColumnName("Ending_year");
            });

            modelBuilder.Entity<Account>(entity =>
            {
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

            modelBuilder.Entity<Audience>(entity =>
            {
                entity.Property(e => e.AudienceId).HasColumnName("Audience_id");

                entity.Property(e => e.EmployeeHeadId).HasColumnName("Employee_head_id");

                entity.Property(e => e.Name).HasMaxLength(65);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.EmployeeHead)
                    .WithMany(p => p.Audiences)
                    .HasForeignKey(d => d.EmployeeHeadId)
                    .HasConstraintName("FK_Audiences_Employees");
            });

            modelBuilder.Entity<Cathedra>(entity =>
            {
                entity.Property(e => e.CathedraId).HasColumnName("Cathedra_id");

                entity.Property(e => e.Abbreviation).HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<CathedraSpecialty>(entity =>
            {
                entity.HasKey(e => new { e.CathedraId, e.SpecialtieId });

                entity.ToTable("Cathedra_specialties");

                entity.Property(e => e.CathedraId).HasColumnName("Cathedra_id");

                entity.Property(e => e.SpecialtieId).HasColumnName("Specialtie_id");

                entity.HasOne(d => d.Cathedra)
                    .WithMany(p => p.CathedraSpecialties)
                    .HasForeignKey(d => d.CathedraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cathedra_specialties_Cathedras");

                entity.HasOne(d => d.Specialtie)
                    .WithMany(p => p.CathedraSpecialties)
                    .HasForeignKey(d => d.SpecialtieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cathedra_specialties_Specialties");
            });

            modelBuilder.Entity<ConductedPair>(entity =>
            {
                entity.ToTable("Conducted_pairs");

                entity.Property(e => e.ConductedPairId).HasColumnName("Conducted_pair_id");

                entity.Property(e => e.LessonType).HasColumnName("Lesson_type");

                entity.Property(e => e.ScheduleDisciplineId).HasColumnName("Schedule_discipline_id");

                entity.Property(e => e.ScheduleDisciplineReplacementId).HasColumnName("Schedule_discipline_replacement_id");

                entity.HasOne(d => d.LessonTypeNavigation)
                    .WithMany(p => p.ConductedPairs)
                    .HasForeignKey(d => d.LessonType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conducted_pairs_Lesson_types");

                entity.HasOne(d => d.ScheduleDiscipline)
                    .WithMany(p => p.ConductedPairs)
                    .HasForeignKey(d => d.ScheduleDisciplineId)
                    .HasConstraintName("FK_Conducted_pairs_Schedule_disciplines1");

                entity.HasOne(d => d.ScheduleDisciplineReplacement)
                    .WithMany(p => p.ConductedPairs)
                    .HasForeignKey(d => d.ScheduleDisciplineReplacementId)
                    .HasConstraintName("FK_Conducted_pairs_Schedule_discipline_replacement");
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.Property(e => e.DisciplineId).HasColumnName("Discipline_id");

                entity.Property(e => e.CathedraId).HasColumnName("Cathedra_id");

                entity.Property(e => e.DisciplineIndex)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Discipline_index");

                entity.Property(e => e.EducationCycleId).HasColumnName("Education_cycle_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(125);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(d => d.Cathedra)
                    .WithMany(p => p.Disciplines)
                    .HasForeignKey(d => d.CathedraId)
                    .HasConstraintName("FK_Disciplines_Cathedras");

                entity.HasOne(d => d.EducationCycle)
                    .WithMany(p => p.Disciplines)
                    .HasForeignKey(d => d.EducationCycleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Disciplines_Education_cycles_and_modules");
            });

            modelBuilder.Entity<EducationCyclesAndModule>(entity =>
            {
                entity.HasKey(e => e.EducationCycleId);

                entity.ToTable("Education_cycles_and_modules");

                entity.Property(e => e.EducationCycleId).HasColumnName("Education_cycle_id");

                entity.Property(e => e.EducationCycleIndex)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Education_cycle_index");

                entity.Property(e => e.EducationCycleParentId).HasColumnName("Education_cycle_parent_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.HasOne(d => d.EducationCycleParent)
                    .WithMany(p => p.InverseEducationCycleParent)
                    .HasForeignKey(d => d.EducationCycleParentId)
                    .HasConstraintName("FK_Education_cycles_and_modules_Education_cycles_and_modules");
            });

            modelBuilder.Entity<EducationForm>(entity =>
            {
                entity.ToTable("Education_forms");

                entity.Property(e => e.EducationFormId).HasColumnName("Education_form_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EducationLevel>(entity =>
            {
                entity.ToTable("Education_levels");

                entity.Property(e => e.EducationLevelId).HasColumnName("Education_level_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<EducationPlan>(entity =>
            {
                entity.ToTable("Education_plans");

                entity.Property(e => e.EducationPlanId).HasColumnName("Education_plan_id");

                entity.Property(e => e.AcademicYearId).HasColumnName("Academic_year_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(65);

                entity.Property(e => e.SpecialtieId).HasColumnName("Specialtie_id");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.EducationPlans)
                    .HasForeignKey(d => d.AcademicYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Education_plans_Academic_years");

                entity.HasOne(d => d.Specialtie)
                    .WithMany(p => p.EducationPlans)
                    .HasForeignKey(d => d.SpecialtieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Education_plans_Specialties");
            });

            modelBuilder.Entity<EducationPlanSemesterDiscipline>(entity =>
            {
                entity.HasKey(e => new { e.EducationPlanId, e.SemesterDisciplineId });

                entity.ToTable("Education_plan_semester_disciplines");

                entity.Property(e => e.EducationPlanId).HasColumnName("Education_plan_id");

                entity.Property(e => e.SemesterDisciplineId).HasColumnName("Semester_discipline_id");

                entity.HasOne(d => d.EducationPlan)
                    .WithMany(p => p.EducationPlanSemesterDisciplines)
                    .HasForeignKey(d => d.EducationPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Education_plan_semester_disciplines_Education_plans");

                entity.HasOne(d => d.SemesterDiscipline)
                    .WithMany(p => p.EducationPlanSemesterDisciplines)
                    .HasForeignKey(d => d.SemesterDisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Education_plan_semester_disciplines_Semester_disciplines");
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

            modelBuilder.Entity<FixedDiscipline>(entity =>
            {
                entity.ToTable("Fixed_disciplines");

                entity.Property(e => e.FixedDisciplineId).HasColumnName("Fixed_discipline_id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.GroupId).HasColumnName("Group_id");

                entity.Property(e => e.IsAgreed).HasColumnName("Is_agreed");

                entity.Property(e => e.SemesterDisciplineId).HasColumnName("Semester_discipline_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.FixedDisciplines)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fixed_disciplines_Employees");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.FixedDisciplines)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fixed_disciplines_Groups");

                entity.HasOne(d => d.SemesterDiscipline)
                    .WithMany(p => p.FixedDisciplines)
                    .HasForeignKey(d => d.SemesterDisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fixed_disciplines_Semester_disciplines");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.GroupId).HasColumnName("Group_id");

                entity.Property(e => e.CourseNumber).HasColumnName("Course_number");

                entity.Property(e => e.CuratorId).HasColumnName("Curator_id");

                entity.Property(e => e.EducationPlanId).HasColumnName("Education_plan_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ReceiptYear).HasColumnName("Receipt_year");

                entity.Property(e => e.ReceivedEducationId).HasColumnName("Received_education_id");

                entity.HasOne(d => d.Curator)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CuratorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_Employees");

                entity.HasOne(d => d.EducationPlan)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.EducationPlanId)
                    .HasConstraintName("FK_Groups_Education_plans");

                entity.HasOne(d => d.ReceivedEducation)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.ReceivedEducationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_Received_educations");
            });

            modelBuilder.Entity<IntermediateCertificationForm>(entity =>
            {
                entity.HasKey(e => e.CertificationFormId);

                entity.ToTable("Intermediate_certification_forms");

                entity.Property(e => e.CertificationFormId).HasColumnName("Certification_form_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<LessonType>(entity =>
            {
                entity.ToTable("Lesson_types");

                entity.Property(e => e.LessonTypeId).HasColumnName("Lesson_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(65);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId).HasColumnName("Post_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<ReceivedEducation>(entity =>
            {
                entity.ToTable("Received_educations");

                entity.Property(e => e.ReceivedEducationId).HasColumnName("Received_education_id");

                entity.Property(e => e.EducationLevelId).HasColumnName("Education_level_id");

                entity.Property(e => e.IsBudget).HasColumnName("Is_budget");

                entity.Property(e => e.ReceivedEducationFormId).HasColumnName("Received_education_form_id");

                entity.Property(e => e.ReceivedSpecialtyId).HasColumnName("Received_specialty_id");

                entity.Property(e => e.StudyPeriodMonths).HasColumnName("Study_period_months");

                entity.HasOne(d => d.EducationLevel)
                    .WithMany(p => p.ReceivedEducations)
                    .HasForeignKey(d => d.EducationLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Received_educations_Education_levels");

                entity.HasOne(d => d.ReceivedEducationForm)
                    .WithMany(p => p.ReceivedEducations)
                    .HasForeignKey(d => d.ReceivedEducationFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Received_educations_Received_education_forms");

                entity.HasOne(d => d.ReceivedSpecialty)
                    .WithMany(p => p.ReceivedEducations)
                    .HasForeignKey(d => d.ReceivedSpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Received_educations_Received_specialties");
            });

            modelBuilder.Entity<ReceivedEducationForm>(entity =>
            {
                entity.ToTable("Received_education_forms");

                entity.Property(e => e.ReceivedEducationFormId).HasColumnName("Received_education_form_id");

                entity.Property(e => e.AdditionalInfo)
                    .HasMaxLength(65)
                    .HasColumnName("Additional_info");

                entity.Property(e => e.EducationFormId).HasColumnName("Education_form_id");

                entity.HasOne(d => d.EducationForm)
                    .WithMany(p => p.ReceivedEducationForms)
                    .HasForeignKey(d => d.EducationFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Received_education_forms_Education_forms");
            });

            modelBuilder.Entity<ReceivedSpecialty>(entity =>
            {
                entity.ToTable("Received_specialties");

                entity.Property(e => e.ReceivedSpecialtyId).HasColumnName("Received_specialty_id");

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SpecialtieId).HasColumnName("Specialtie_id");

                entity.HasOne(d => d.Specialtie)
                    .WithMany(p => p.ReceivedSpecialties)
                    .HasForeignKey(d => d.SpecialtieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Received_specialties_Specialties");
            });

            modelBuilder.Entity<ScheduleDiscipline>(entity =>
            {
                entity.ToTable("Schedule_disciplines");

                entity.Property(e => e.ScheduleDisciplineId).HasColumnName("Schedule_discipline_id");

                entity.Property(e => e.AudienceId).HasColumnName("Audience_id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FixedDisciplineId).HasColumnName("Fixed_discipline_id");

                entity.Property(e => e.IsEvenPair).HasColumnName("Is_even_pair");

                entity.Property(e => e.IsFirstSubgroup).HasColumnName("Is_first_subgroup");

                entity.Property(e => e.PairNumber).HasColumnName("Pair_number");

                entity.HasOne(d => d.Audience)
                    .WithMany(p => p.ScheduleDisciplines)
                    .HasForeignKey(d => d.AudienceId)
                    .HasConstraintName("FK_Schedule_disciplines_Audiences");

                entity.HasOne(d => d.FixedDiscipline)
                    .WithMany(p => p.ScheduleDisciplines)
                    .HasForeignKey(d => d.FixedDisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_disciplines_Fixed_disciplines");
            });

            modelBuilder.Entity<ScheduleDisciplineReplacement>(entity =>
            {
                entity.HasKey(e => e.SheduleDisciplineReplacementId);

                entity.ToTable("Schedule_discipline_replacement");

                entity.Property(e => e.SheduleDisciplineReplacementId)
                    .ValueGeneratedNever()
                    .HasColumnName("Shedule_discipline_replacement_id");

                entity.Property(e => e.AudienceId).HasColumnName("Audience_id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FixedDisciplineId).HasColumnName("Fixed_discipline_id");

                entity.Property(e => e.IsFirstSubgroup).HasColumnName("Is_first_subgroup");

                entity.Property(e => e.PairNumber).HasColumnName("Pair_number");

                entity.Property(e => e.ScheduleDisciplineId).HasColumnName("Schedule_discipline_id");

                entity.HasOne(d => d.Audience)
                    .WithMany(p => p.ScheduleDisciplineReplacements)
                    .HasForeignKey(d => d.AudienceId)
                    .HasConstraintName("FK_Schedule_discipline_replacement_Audiences");

                entity.HasOne(d => d.FixedDiscipline)
                    .WithMany(p => p.ScheduleDisciplineReplacements)
                    .HasForeignKey(d => d.FixedDisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_discipline_replacement_Fixed_disciplines");

                entity.HasOne(d => d.ScheduleDiscipline)
                    .WithMany(p => p.ScheduleDisciplineReplacements)
                    .HasForeignKey(d => d.ScheduleDisciplineId)
                    .HasConstraintName("FK_Schedule_discipline_replacement_Schedule_disciplines");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.Property(e => e.SemesterId).HasColumnName("Semester_id");

                entity.Property(e => e.WeeksCount).HasColumnName("Weeks_count");
            });

            modelBuilder.Entity<SemesterDiscipline>(entity =>
            {
                entity.ToTable("Semester_disciplines");

                entity.Property(e => e.SemesterDisciplineId).HasColumnName("Semester_discipline_id");

                entity.Property(e => e.CertificationFormId).HasColumnName("Certification_form_id");

                entity.Property(e => e.ConsultationHours).HasColumnName("Consultation_hours");

                entity.Property(e => e.ControlWorkHours).HasColumnName("Control_work_hours");

                entity.Property(e => e.DisciplineId).HasColumnName("Discipline_id");

                entity.Property(e => e.EducationalPracticeHours).HasColumnName("Educational_practice_hours");

                entity.Property(e => e.ExamHours).HasColumnName("Exam_hours");

                entity.Property(e => e.IndependentWorkHours).HasColumnName("Independent_work_hours");

                entity.Property(e => e.LaboratoryWorkHours).HasColumnName("Laboratory_work_hours");

                entity.Property(e => e.PracticeWorkHours).HasColumnName("Practice_work_hours");

                entity.Property(e => e.ProductionPracticeHours).HasColumnName("Production_practice_hours");

                entity.Property(e => e.SemesterId).HasColumnName("Semester_id");

                entity.Property(e => e.TheoryLessonHours).HasColumnName("Theory_lesson_hours");

                entity.HasOne(d => d.CertificationForm)
                    .WithMany(p => p.SemesterDisciplines)
                    .HasForeignKey(d => d.CertificationFormId)
                    .HasConstraintName("FK_Semester_disciplines_Intermediate_certification_forms");

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.SemesterDisciplines)
                    .HasForeignKey(d => d.DisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semesters_Disciplines_Disciplines");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.SemesterDisciplines)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Semesters_Disciplines_Semesters");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasKey(e => e.SpecialtieId);

                entity.Property(e => e.SpecialtieId).HasColumnName("Specialtie_id");

                entity.Property(e => e.Abbreviation).HasMaxLength(10);

                entity.Property(e => e.ImplementedSpecialtyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Implemented_specialty_name");

                entity.Property(e => e.SpecialtieCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Specialtie_code");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
