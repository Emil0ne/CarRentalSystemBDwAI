using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Office
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa oddziału jest wymagana")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj miasto")]
        public string City { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
    }
}