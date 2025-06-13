using Microsoft.Extensions.Logging;

namespace Read_Win.Models
{
    public class SchoolYear
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public required string Period { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrent { get; set; }

        public ICollection<StudentClassMapping> StudentMappings { get; set; } = new List<StudentClassMapping>();
        public ICollection<ClassRating> ClassRatings { get; set; } = new List<ClassRating>();
    }
}
