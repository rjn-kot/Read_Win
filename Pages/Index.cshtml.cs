using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Read_Win.Data;
using Read_Win.Models;
using System.ComponentModel.DataAnnotations;

namespace Read_Win.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EventInputModel EventInput { get; set; } = new EventInputModel();

        public class EventInputModel
        {
            [Required(ErrorMessage = "Название обязательно")]
            [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "Дата обязательна")]
            [DataType(DataType.DateTime)]
            public DateTime Date { get; set; } = DateTime.Now;

            public string? Description { get; set; }

            [Required(ErrorMessage = "Тип обязателен")]
            public EventType Type { get; set; }

            public List<EventParallelInput> Parallels { get; set; } = new();
        }
        public class EventParallelInput
        {
            [Range(1, 4, ErrorMessage = "Параллель должна быть от 1 до 4")]
            public int ClassParallel { get; set; }

            [Required(ErrorMessage = "Название книги обязательно")]
            public string BookName { get; set; } = string.Empty;
        }

        public IActionResult OnGet()
        {
            // По умолчанию добавляем одну пустую параллель
            EventInput.Parallels.Add(new EventParallelInput());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Логируем полученные данные
            Console.WriteLine($"Получены данные для создания мероприятия:");
            Console.WriteLine($"Название: {EventInput.Name}");
            Console.WriteLine($"Дата: {EventInput.Date}");
            Console.WriteLine($"Описание: {EventInput.Description}");
            Console.WriteLine($"Тип: {EventInput.Type}");

            Console.WriteLine("Параллели:");
            foreach (var parallel in EventInput.Parallels)
            {
                Console.WriteLine($"- Класс: {parallel.ClassParallel}, Книга: {parallel.BookName}");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Ошибки валидации:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
                return Page();
            }

            var newEvent = new Models.Event
            {
                Name = EventInput.Name,
                Date = EventInput.Date,
                Description = EventInput.Description,
                Type = EventInput.Type,
            };

            foreach (var parallel in EventInput.Parallels)
            {
                newEvent.EventParallels.Add(new EventParallel
                {
                    ClassParallel = parallel.ClassParallel,
                    BookName = parallel.BookName
                });
            }

            _context.Event.Add(newEvent);
            await _context.SaveChangesAsync();

            Console.WriteLine("Мероприятие успешно создано!");
            return RedirectToPage("./Index");
        }
    }
}