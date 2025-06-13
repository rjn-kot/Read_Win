namespace Read_Win.Models
{
    public class ClassRating
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int YearId { get; set; }
        public int Points { get; set; }

        public SchoolClass SchoolClass { get; set; } = null!;
        public SchoolYear SchoolYear { get; set; } = null!;
    }

    public class StudentRating
    {
        public int Id { get; set; }
        public int MappingId { get; set; }
        public int Points { get; set; }

        public StudentClassMapping Mapping { get; set; } = null!;
    }
}
