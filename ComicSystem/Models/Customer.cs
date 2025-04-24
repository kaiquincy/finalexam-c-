using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ComicSystem.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }

        [ValidateNever]
        public ICollection<Rental> Rentals { get; set; }
    }
}
