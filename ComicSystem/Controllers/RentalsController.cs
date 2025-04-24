using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RentalsController(ApplicationDbContext context) => _context = context;

        // GET: /Rentals/Create?customerId=1
        public async Task<IActionResult> Create(int customerId)
        {
            var vm = new RentalCreateViewModel
            {
                Customer = await _context.Customers.FindAsync(customerId),
                ComicBooks = await _context.ComicBooks.ToListAsync(),
                RentalDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(1)
            };
            return View(vm);
        }

        // POST: /Rentals/Create
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Create(RentalCreateViewModel vm)
{
    if (!ModelState.IsValid)
    {
        // nạp lại để view không bị null
        vm.Customer    = await _context.Customers.FindAsync(vm.CustomerId);
        vm.ComicBooks  = await _context.ComicBooks.ToListAsync();
        return View(vm);
    }

    // nếu hợp lệ: tạo entity và lưu
    var rental = new Rental {
        CustomerId    = vm.CustomerId,
        RentalDate    = vm.RentalDate,
        ReturnDate    = vm.ReturnDate,
        RentalDetails = new List<RentalDetail> {
            new RentalDetail {
                ComicBookId = vm.SelectedComicBookId,
                Quantity    = vm.Quantity,
                PricePerDay = vm.PricePerDay
            }
        }
    };

    _context.Rentals.Add(rental);
    await _context.SaveChangesAsync();
    return RedirectToAction("Index", "Reports");
}


    }
}
