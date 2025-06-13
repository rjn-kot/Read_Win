using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Read_Win.Data;
using Read_Win.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static Read_Win.Pages.IndexModel;

namespace Read_Win.Pages.Event
{
    public class EventModel : PageModel
    {
        private readonly AppDbContext _context;

        public EventModel(AppDbContext context)
        {
            _context = context;
        }

        public List<SchoolYear> SchoolYears { get; set; } = new List<SchoolYear>();
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();

        [BindProperty(SupportsGet = true)]
        public string SelectedPeriod { get; set; }

        public async Task OnGetAsync()
        {
            // �������� ������ ���� ������� �����
            SchoolYears = await _context.SchoolYear
                .OrderByDescending(y => y.Id)
                .ToListAsync();

            // �������� ������� ��� (IsCurrent = true) ��� ������ ��������� ���
            var defaultYear = await _context.SchoolYear
                .FirstOrDefaultAsync(y => y.IsCurrent) ?? SchoolYears.FirstOrDefault();

            SelectedPeriod ??= defaultYear?.Period;
            ViewData["DefaultOption"] = defaultYear?.Period;

            // ��������� ������� �� ���������� �������
            await LoadEventsAsync(SelectedPeriod);
        }

        public async Task<IActionResult> OnGetFilterEventsAsync(string period)
        {
            await LoadEventsAsync(period);
            return Partial("_EventPartial", Events);
        }

        private async Task LoadEventsAsync(string period)
        {
            // ������� ������� ��� �� ���������� �������
            var schoolYear = await _context.SchoolYear
                .FirstOrDefaultAsync(y => y.Period == period);

            if (schoolYear == null)
            {
                Events = new List<EventViewModel>();
                return;
            }

            Events = await _context.Event
                .Include(e => e.EventParallels)
                .Where(e => e.Date >= schoolYear.StartDate && e.Date <= schoolYear.EndDate)
                .OrderBy(e => e.Date)
                .Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Title = e.Name,
                    Date = e.Date,
                    Type = e.Type.ToString(),
                    Status = e.Status.ToString(),
                    ClassParallels = e.EventParallels
                        .Select(ep => ep.ClassParallel)
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostCreateEventAsync([FromBody] EventInputModel input)
        {
            try
            {
                Console.WriteLine($"�������� ������: Name={input.Name}, Date={input.Date}, Type={input.Type}");

                // ������ ���� �� ������� "dd.MM.yyyy HH:mm:ss"
                if (!DateTime.TryParseExact(input.Date, "dd.MM.yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var eventDate))
                {
                    return BadRequest("�������� ������ ����. ��������� dd.MM.yyyy HH:mm:ss");
                }

                // ��������� ��� �����������
                if (!Enum.TryParse<EventType>(input.Type, out var eventType))
                {
                    return BadRequest("�������� ��� �����������");
                }

                // ��������� ������ � ������� � �������������
                if (input.ClassWorks == null || input.ClassWorks.Count == 0)
                {
                    return BadRequest("���������� ������� ���� �� ���� ����� � ������������");
                }

                var newEvent = new Models.Event
                {
                    Name = input.Name,
                    Date = eventDate,
                    Type = eventType,
                    Description = input.Description,
                    EventParallels = input.ClassWorks.Select(cw => new EventParallel
                    {
                        ClassParallel = cw.ClassParallel,
                        BookName = cw.BookName
                    }).ToList()
                };

                _context.Event.Add(newEvent);
                await _context.SaveChangesAsync();

                return new JsonResult(new { success = true, message = "����������� ������� �������" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"������: {ex.Message}");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
    public class EventViewModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime Date { get; set; }
        public required string Type { get; set; }
        public required string Status { get; set; }
        public List<int> ClassParallels { get; set; } = new List<int>();

        public string GetFormattedDate()
        {
            var culture = new CultureInfo("ru-RU");
            var dateString = Date.ToString("d MMMM", culture);
            return dateString;
        }

        public string FormatClassParallel(int parallel)
        {
            return $"{parallel} �����";
        }

        public string FormatShortClassParallel(int parallel)
        {
            return $"{parallel}��";
        }
    }
    public class EventInputModel
    {
        [Required(ErrorMessage = "�������� �����������")]
        public string Name { get; set; }

        [Required(ErrorMessage = "���� �����������")]
        public string Date { get; set; }

        [Required(ErrorMessage = "��� ����������")]
        public string Type { get; set; }

        public string Description { get; set; }

        public List<ClassWorkInputModel> ClassWorks { get; set; } = new List<ClassWorkInputModel>();
    }

    public class ClassWorkInputModel
    {
        [Range(1, 4, ErrorMessage = "��������� ������ ���� �� 1 �� 4")]
        public int ClassParallel { get; set; }

        [Required(ErrorMessage = "�������� ����� �����������")]
        public string BookName { get; set; }
    }

}

