using System.ComponentModel.DataAnnotations;


namespace ComicSystem.Models
{
    public class Rental
    {
        public int RentalId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [DataType(DataType.Date)]
        public DateTime RentalDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }

        public ICollection<RentalDetail> RentalDetails { get; set; }
    }
}
