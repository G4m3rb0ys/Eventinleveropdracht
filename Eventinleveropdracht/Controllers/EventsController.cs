using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventinleveropdracht.Data;
using Eventinleveropdracht.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eventinleveropdracht.Controllers
{
    public class EventsController : Controller
    {
        private readonly VoorbeeldDatabase _context;

        public EventsController(VoorbeeldDatabase context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var voorbeeldDatabase = _context.Events.Include(n => n.Organiser);
            return View(await voorbeeldDatabase.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Haal het event op met reserveringen
            var @event = await _context.Events
                .Include(e => e.Organiser)
                .Include(e => e.Reservations) // Zorg dat reserveringen geladen worden
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            // Zoekfilter toepassen op de reserveringen
            if (!string.IsNullOrEmpty(searchString))
            {
                @event.Reservations = @event.Reservations
                    .Where(r => r.Name.ToLower().Contains(searchString.ToLower()))
                    .ToList();
            }

            ViewData["CurrentFilter"] = searchString;

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            ViewData["OrganiserId"] = new SelectList(_context.Organizers, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,FromDate,ToDate,Location,Type,Requirements,MaxParticipants,CurrentParticipants,Image,OrganiserId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganiserId"] = new SelectList(_context.Organizers, "Id", "Name", @event.OrganiserId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["OrganiserId"] = new SelectList(_context.Organizers, "Id", "Name", @event.OrganiserId);
            return View(@event);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,FromDate,ToDate,Location,Type,Requirements,MaxParticipants,CurrentParticipants,Image,OrganiserId")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganiserId"] = new SelectList(_context.Organizers, "Id", "Name", @event.OrganiserId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(n => n.Organiser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        // POST: Reservering maken
        [HttpPost]
        public IActionResult Reserve(Reservatie reservatie)
        {
            if (ModelState.IsValid)
            {
                reservatie.ReservationNumber = new Random().Next(100000, 999999);
                reservatie.Paid = false;
                reservatie.Date = DateTime.Now;

                _context.Reservaties.Add(reservatie);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
