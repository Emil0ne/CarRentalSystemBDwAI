using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Marka jest obowiązkowa")]
        [Display(Name = "Marka")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model jest obowiązkowy")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Range(1900, 2026, ErrorMessage = "Podaj poprawny rok")]
        [Display(Name = "Rok produkcji")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Cena za dobę")]
        [DisplayFormat(DataFormatString = "{0:0} zł", ApplyFormatInEditMode = false)]
        public decimal PricePerDay { get; set; }

        [Display(Name = "ID Oddziału")]
        public int OfficeId { get; set; }

        [Display(Name = "Oddział")]
        public virtual Office? Office { get; set; }
    }
}