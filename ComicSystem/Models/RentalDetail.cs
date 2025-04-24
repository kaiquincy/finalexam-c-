using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class RentalDetail
    {
        public int RentalDetailId { get; set; }

        [Required]
        public int RentalId { get; set; }
        public Rental Rental { get; set; }

        [Required]
        public int ComicBookId { get; set; }
        public ComicBook ComicBook { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal PricePerDay { get; set; }
    }
}
