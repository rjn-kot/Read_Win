using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Read_Win.Models;

namespace Read_Win.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<SchoolYear> SchoolYear { get; set; } = null!;
        public DbSet<SchoolClass> SchoolClass { get; set; } = null!;
        public DbSet<Student> Student { get; set; } = null!;
        public DbSet<StudentClassMapping> StudentClassMapping { get; set; } = null!;
        public DbSet<ClassRating> ClassRating { get; set; } = null!;
        public DbSet<StudentRating> StudentRating { get; set; } = null!;
        public DbSet<Event> Event { get; set; } = null!;
        public DbSet<EventParallel> EventParallel { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SchoolClass configuration
            modelBuilder.Entity<SchoolClass>(entity =>
            {
                entity.ToTable("school_class", t =>
                {
                    t.HasCheckConstraint("school_class_chk_1", "class_number BETWEEN 1 AND 4");
                });

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.ClassNumber)
                    .HasColumnName("class_number");

                entity.Property(e => e.ClassLetter)
                    .HasColumnName("class_letter")
                    .HasMaxLength(1);

                entity.HasIndex(e => new { e.ClassNumber, e.ClassLetter })
                    .HasDatabaseName("class_number")
                    .IsUnique();
            });

            // SchoolYear configuration
            modelBuilder.Entity<SchoolYear>(entity =>
            {
                entity.ToTable("school_year", t =>
                {
                    t.HasCheckConstraint("date_check", "end_date > start_date");
                });

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Period)
                    .HasColumnName("period")
                    .HasMaxLength(10);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.IsCurrent)
                    .HasColumnName("is_current")
                    .HasDefaultValue(false);

                entity.HasIndex(e => e.IsCurrent)
                    .HasDatabaseName("idx_school_year_is_current");

                entity.HasIndex(e => new { e.StartDate, e.EndDate })
                    .HasDatabaseName("idx_school_year_dates");
            });

            // Student configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.EnrollmentDate)
                    .HasColumnName("enrollment_date")
                    .HasColumnType("date")
                    .HasComment("Дата зачисления студента (не может быть NULL)");
            });

            // StudentClassMapping configuration
            modelBuilder.Entity<StudentClassMapping>(entity =>
            {
                entity.ToTable("student_class_mapping");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.StudentId)
                    .HasColumnName("student_id");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id");

                entity.Property(e => e.YearId)
                    .HasColumnName("year_id");

                entity.HasIndex(e => new { e.StudentId, e.YearId })
                    .HasDatabaseName("student_id")
                    .IsUnique();

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.ClassMappings)
                    .HasForeignKey(e => e.StudentId)
                    .HasConstraintName("student_class_mapping_ibfk_1")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.SchoolClass)
                    .WithMany()
                    .HasForeignKey(e => e.ClassId)
                    .HasConstraintName("student_class_mapping_ibfk_2")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.SchoolYear)
                    .WithMany()
                    .HasForeignKey(e => e.YearId)
                    .HasConstraintName("student_class_mapping_ibfk_3")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // StudentRating configuration
            modelBuilder.Entity<StudentRating>(entity =>
            {
                entity.ToTable("student_rating");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.MappingId)
                    .HasColumnName("mapping_id");

                entity.Property(e => e.Points)
                    .HasColumnName("points")
                    .HasDefaultValue(0);

                entity.HasOne(e => e.Mapping)
                    .WithMany(m => m.StudentRatings)
                    .HasForeignKey(e => e.MappingId)
                    .HasConstraintName("student_rating_ibfk_1")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ClassRating configuration
            modelBuilder.Entity<ClassRating>(entity =>
            {
                entity.ToTable("class_rating");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.ClassId)
                    .HasColumnName("class_id");

                entity.Property(e => e.YearId)
                    .HasColumnName("year_id");

                entity.Property(e => e.Points)
                    .HasColumnName("points")
                    .HasDefaultValue(0);

                entity.HasIndex(e => new { e.ClassId, e.YearId })
                    .HasDatabaseName("class_year_unique")
                    .IsUnique();

                entity.HasOne(e => e.SchoolClass)
                    .WithMany(c => c.ClassRatings)
                    .HasForeignKey(e => e.ClassId)
                    .HasConstraintName("class_rating_ibfk_1")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.SchoolYear)
                    .WithMany()
                    .HasForeignKey(e => e.YearId)
                    .HasConstraintName("class_rating_ibfk_2")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Event configuration
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Date)
                    .HasColumnName("date");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValue(EventStatus.Будет)
                    .HasConversion<string>()
                    .HasMaxLength(50);
                entity.Property(e => e.Description)
                    .HasColumnName("description");
            });

            // EventClass configuration
            modelBuilder.Entity<EventParallel>(entity =>
            {
                entity.ToTable("event_class");  // Сохраняем старое название таблицы

                entity.Property(e => e.EventId)
                    .HasColumnName("event_id");

                entity.Property(e => e.ClassParallel)
                    .HasColumnName("class_parallel");

                entity.Property(e => e.BookName)
                    .HasColumnName("book_name")
                    .HasMaxLength(255);

                entity.HasKey(e => new { e.EventId, e.ClassParallel });

                entity.HasOne(ep => ep.Event)
                    .WithMany(e => e.EventParallels)
                    .HasForeignKey(ep => ep.EventId)
                    .HasConstraintName("fk_event_parallel")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}