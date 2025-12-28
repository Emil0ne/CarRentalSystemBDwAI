using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Marka jest obowiązkowa")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model jest obowiązkowy")]
        public string Model { get; set; }

        [Range(1900, 2026, ErrorMessage = "Podaj poprawny rok")]
        public int Year { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }

        public int OfficeId { get; set; }
        public virtual Office? Office { get; set; }
    }
}