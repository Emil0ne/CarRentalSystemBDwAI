using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int CarId { get; set; }
        public virtual Car? Car { get; set; }

        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }
}