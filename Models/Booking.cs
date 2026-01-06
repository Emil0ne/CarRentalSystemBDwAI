using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CarRentalSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Data rozpoczęcia jest wymagana")]
        [DataType(DataType.Date)]
        [Display(Name = "Data rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Data zakończenia jest wymagana")]
        [DataType(DataType.Date)]
        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Auto")]
        public int CarId { get; set; }

        [Display(Name = "Auto")]
        public virtual Car? Car { get; set; }

        public int? OfficeId { get; set; }
        public virtual Office? Office { get; set; }

        [Display(Name = "Klient")]
        public string? ClientId { get; set; }

        [Display(Name = "Klient")]
        public virtual IdentityUser? Client { get; set; }
    }
}