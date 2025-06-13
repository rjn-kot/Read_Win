using System.ComponentModel.DataAnnotations.Schema;

namespace Read_Win.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public byte ClassNumber { get; set; }
        public required string ClassLetter { get; set; }

        public ICollection<StudentClassMapping> StudentMappings { get; set; } = new List<StudentClassMapping>();
        public ICollection<ClassRating> ClassRatings { get; set; } = new List<ClassRating>();
    }
}
