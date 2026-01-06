using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarRentalSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}