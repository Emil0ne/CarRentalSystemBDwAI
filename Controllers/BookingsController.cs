using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalSystem.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Client);
            return View(await bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Client)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name");

            var carsList = _context.Cars.Select(c => new {
                id = c.Id,
                text = c.Brand + " " + c.Model,
                officeId = c.OfficeId
            }).ToList();

            ViewBag.CarsJson = System.Text.Json.JsonSerializer.Serialize(carsList);

            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,CarId,OfficeId")] Booking booking)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            booking.ClientId = userId;

            if (booking.StartDate < DateTime.Today)
            {
                ModelState.AddModelError("StartDate", "Data rozpoczęcia nie może być wcześniejsza niż dzisiaj.");
            }

            if (booking.EndDate < booking.StartDate)
            {
                ModelState.AddModelError("EndDate", "Data zakończenia nie może być wcześniejsza niż rozpoczęcia.");
            }

            ModelState.Remove("ClientId");
            ModelState.Remove("Client");

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", booking.OfficeId);
            var carsList = _context.Cars.Select(c => new { id = c.Id, text = c.Brand + " " + c.Model, officeId = c.OfficeId }).ToList();
            ViewBag.CarsJson = System.Text.Json.JsonSerializer.Serialize(carsList);

            return View(booking);
        }

        // GET: Bookings/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", booking.OfficeId);

            var carsList = _context.Cars.Select(c => new {
                id = c.Id,
                text = c.Brand + " " + c.Model,
                officeId = c.OfficeId
            }).ToList();
            ViewBag.CarsJson = System.Text.Json.JsonSerializer.Serialize(carsList);

            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,CarId,OfficeId,ClientId")] Booking booking)
        {
            if (id != booking.Id) return NotFound();

            if (booking.StartDate < DateTime.Today)
            {
                ModelState.AddModelError("StartDate", "Nie można zmienić rezerwacji na datę z przeszłości.");
            }

            if (booking.EndDate < booking.StartDate)
            {
                ModelState.AddModelError("EndDate", "Data zakończenia nie może być wcześniejsza niż rozpoczęcia.");
            }

            ModelState.Remove("Client");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id)) return NotFound();
                    else throw;
                }
            }

            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Name", booking.OfficeId);
            var carsList = _context.Cars.Select(c => new { id = c.Id, text = c.Brand + " " + c.Model, officeId = c.OfficeId }).ToList();
            ViewBag.CarsJson = System.Text.Json.JsonSerializer.Serialize(carsList);

            return View(booking);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Car)
                .Include(b => b.Client)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}