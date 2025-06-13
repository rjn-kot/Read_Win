using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Read_Win.Data;
using Read_Win.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Read_Win.Pages.Top
{
    public class TopModel : PageModel
    {
        private readonly AppDbContext _context;

        public TopModel(AppDbContext context)
        {
            _context = context;
        }

        public List<SchoolYear> SchoolYears { get; set; } = new List<SchoolYear>();

        public async Task OnGetAsync()
        {
            var currentYear = await _context.SchoolYear
                .FirstOrDefaultAsync(y => y.IsCurrent);

            SchoolYears = await _context.SchoolYear
                .OrderByDescending(y => y.Id)
                .ToListAsync();

            ViewData["DefaultOption"] = currentYear?.Period ?? SchoolYears.FirstOrDefault()?.Period;
        }

        public async Task<IActionResult> OnGetRatingDataAsync(string period, int classNumber)
        {
            var ratingsQuery = _context.ClassRating
                .Include(cr => cr.SchoolClass)
                .Include(cr => cr.SchoolYear)
                .Where(cr => cr.SchoolYear.Period == period && cr.SchoolClass.ClassNumber == classNumber)
                .OrderByDescending(cr => cr.Points)
                .Select(cr => new
                {
                    ClassName = $"{cr.SchoolClass.ClassNumber}{cr.SchoolClass.ClassLetter}",
                    Points = cr.Points
                });

            var ratingsList = await ratingsQuery.ToListAsync();

            var result = ratingsList
                .Select((x, i) => new
                {
                    Position = i + 1,
                    x.ClassName,
                    x.Points
                })
                .ToList();

            return Partial("_RatingPartial", result);
        }

        public async Task<IActionResult> OnGetRatingStudDataAsync(string period, int classNumber)
        {
            var ratings = await _context.StudentRating
                .Include(sr => sr.Mapping)
                    .ThenInclude(m => m.Student)
                .Include(sr => sr.Mapping)
                    .ThenInclude(m => m.SchoolClass)
                .Include(sr => sr.Mapping)
                    .ThenInclude(m => m.SchoolYear)
                .Where(sr => sr.Mapping.SchoolYear.Period == period &&
                            sr.Mapping.SchoolClass.ClassNumber == classNumber)
                .OrderByDescending(sr => sr.Points)
                .Select(sr => new
                {
                    Position = _context.StudentRating
                        .Count(sr2 => sr2.Points > sr.Points &&
                                    sr2.Mapping.SchoolYear.Period == period &&
                                    sr2.Mapping.SchoolClass.ClassNumber == classNumber) + 1,
                    StudentName = $"{sr.Mapping.Student.LastName} {sr.Mapping.Student.FirstName}",
                    ClassName = $"{sr.Mapping.SchoolClass.ClassNumber}{sr.Mapping.SchoolClass.ClassLetter}",
                    Points = sr.Points
                })
                .ToListAsync();

            return Partial("_RatingStudPartial", ratings);
        }
    }
}