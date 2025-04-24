using ComicSystem.Data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComicSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context) => _context = context;

        // GET: /Customers/Create
        public IActionResult Create() => View();

        // POST: /Customers/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            customer.RegisterDate = DateTime.Now;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            // Sau khi đăng ký xong, chuyển sang trang tạo đơn thuê
            return RedirectToAction("Create", "Rentals", new { customerId = customer.CustomerId });
        }
    }
}
