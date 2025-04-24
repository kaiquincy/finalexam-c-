using ComicSystem.Data;
using ComicSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context) => _context = context;

        // GET: /Reports
        public async Task<IActionResult> Index(DateTime? from, DateTime? to)
        {
            var query = _context.RentalDetails
                .Include(rd => rd.Rental).ThenInclude(r => r.Customer)
                .Include(rd => rd.ComicBook)
                .AsQueryable();

            if (from.HasValue)
                query = query.Where(rd => rd.Rental.RentalDate >= from.Value);
            if (to.HasValue)
                query = query.Where(rd => rd.Rental.RentalDate <= to.Value);

            var data = await query.Select(rd => new ReportItem
            {
                BookName = rd.ComicBook.Title,
                RentalDate = rd.Rental.RentalDate,
                ReturnDate = rd.Rental.ReturnDate,
                CustomerName = rd.Rental.Customer.FullName,
                Quantity = rd.Quantity
            }).ToListAsync();

            return View(data);
        }
    }
}
