using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Błędny format e-mail")]
        public string Email { get; set; }
    }
}