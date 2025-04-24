using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class ComicBook
    {
        public int ComicBookId { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; }

        [DataType(DataType.Currency)]
        public decimal PricePerDay { get; set; }

        // Đường dẫn tương đối trong wwwroot
        [StringLength(200)]
        public string? ImagePath { get; set; }

        [ValidateNever]
        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
