using System.ComponentModel.DataAnnotations;

namespace Read_Win.Models
{
    public class Event
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public EventType Type { get; set; }
        public EventStatus Status { get; set; }
        public string? Description { get; set; }

        public ICollection<EventParallel> EventParallels { get; set; } = new List<EventParallel>();
    }

    public class EventParallel
    {
        public int EventId { get; set; }
        [Range(1, 4, ErrorMessage = "Параллель должна быть от 1 до 4")]
        public int ClassParallel { get; set; }

        [Required(ErrorMessage = "Название книги обязательно")]
        public string BookName { get; set; } = string.Empty;

        public Event? Event { get; set; }
    }

    public enum EventType
    {
        Индивидуальный,
        Групповой
    }

    public enum EventStatus
    {
        Будет,
        Прошел
    }
}