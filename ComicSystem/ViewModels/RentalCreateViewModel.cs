using ComicSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.ViewModels
{
public class RentalCreateViewModel
{
    public int CustomerId { get; set; }

    // ← dấu ? nghĩa là optional
    public Customer? Customer { get; set; }

    [DataType(DataType.Date)]
    public DateTime RentalDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime ReturnDate { get; set; }

    // ← danh sách sách để build dropdown, cũng optional
    public List<ComicBook>? ComicBooks { get; set; }

    [Required]
    public int SelectedComicBookId { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }

    [DataType(DataType.Currency)]
    public decimal PricePerDay { get; set; }
}

}
