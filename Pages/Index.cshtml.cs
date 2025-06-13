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
            [Required(ErrorMessage = "�������� �����������")]
            [StringLength(100, ErrorMessage = "������������ ����� - 100 ��������")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "���� �����������")]
            [DataType(DataType.DateTime)]
            public DateTime Date { get; set; } = DateTime.Now;

            public string? Description { get; set; }

            [Required(ErrorMessage = "��� ����������")]
            public EventType Type { get; set; }

            public List<EventParallelInput> Parallels { get; set; } = new();
        }
        public class EventParallelInput
        {
            [Range(1, 4, ErrorMessage = "��������� ������ ���� �� 1 �� 4")]
            public int ClassParallel { get; set; }

            [Required(ErrorMessage = "�������� ����� �����������")]
            public string BookName { get; set; } = string.Empty;
        }

        public IActionResult OnGet()
        {
            // �� ��������� ��������� ���� ������ ���������
            EventInput.Parallels.Add(new EventParallelInput());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // �������� ���������� ������
            Console.WriteLine($"�������� ������ ��� �������� �����������:");
            Console.WriteLine($"��������: {EventInput.Name}");
            Console.WriteLine($"����: {EventInput.Date}");
            Console.WriteLine($"��������: {EventInput.Description}");
            Console.WriteLine($"���: {EventInput.Type}");

            Console.WriteLine("���������:");
            foreach (var parallel in EventInput.Parallels)
            {
                Console.WriteLine($"- �����: {parallel.ClassParallel}, �����: {parallel.BookName}");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("������ ���������:");
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

            Console.WriteLine("����������� ������� �������!");
            return RedirectToPage("./Index");
        }
    }
}