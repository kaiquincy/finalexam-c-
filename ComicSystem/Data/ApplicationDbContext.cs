using ComicSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Khóa chính tự động, các quan hệ sẽ được EF Core cấu hình theo tên thuộc tính
        }
    }
}
