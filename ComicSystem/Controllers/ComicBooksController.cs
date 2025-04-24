using ComicSystem.Data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ComicBooksController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: ComicBooks/Create
        public IActionResult Create() => View();

        // POST: ComicBooks/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComicBook book, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
                return View(book);

            // Xử lý upload ảnh nếu có
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await ImageFile.CopyToAsync(stream);

                // Lưu đường dẫn tương đối vào DB
                book.ImagePath = "/images/" + fileName;
            }

            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ComicBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var book = await _context.ComicBooks.FindAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // POST: ComicBooks/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComicBook book, IFormFile? ImageFile)
        {
            if (id != book.ComicBookId) return BadRequest();
            if (!ModelState.IsValid) return View(book);

            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Xóa ảnh cũ (nếu cần)
                if (!string.IsNullOrEmpty(book.ImagePath))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, book.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var uploads = Path.Combine(_env.WebRootPath, "images");
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                using (var stream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    await ImageFile.CopyToAsync(stream);

                book.ImagePath = "/images/" + fileName;
            }

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ComicBooks.Any(e => e.ComicBookId == id))
                    return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ComicBooks/Index
        public async Task<IActionResult> Index()
            => View(await _context.ComicBooks.ToListAsync());

        // GET: ComicBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var book = await _context.ComicBooks.FindAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        // POST: /ComicBooks/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.ComicBooks.FindAsync(id);
            _context.ComicBooks.Remove(book!);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
