using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Office
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa oddziału jest wymagana")]
        [Display(Name = "Nazwa oddziału")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj miasto")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Dostępne samochody")]
        public virtual ICollection<Car>? Cars { get; set; }
    }
}