using System.ComponentModel.DataAnnotations.Schema;

namespace Read_Win.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<StudentClassMapping> ClassMappings { get; set; } = new List<StudentClassMapping>();
    }

    public class StudentClassMapping
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public int YearId { get; set; }

        public Student Student { get; set; } = null!;
        public SchoolClass SchoolClass { get; set; } = null!;
        public SchoolYear SchoolYear { get; set; } = null!;
        public ICollection<StudentRating> StudentRatings { get; set; } = new List<StudentRating>();
    }
}
