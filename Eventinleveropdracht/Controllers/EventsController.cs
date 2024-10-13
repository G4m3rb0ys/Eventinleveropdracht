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
        public async Task<IActionResult> Index(int? SelectedOrganiserId)
        {
            var organisatoren = await _context.Organizers.ToListAsync();
            var events = _context.Events.Include(e => e.Organiser).AsQueryable();

            if (SelectedOrganiserId.HasValue)
            {
                Console.WriteLine($"Selected Organiser ID: {SelectedOrganiserId}");
                events = events.Where(e => e.OrganiserId == SelectedOrganiserId.Value);
            }

            var viewModel = new EventViewModel
            {
                Organisers = new SelectList(organisatoren, "Id", "Name"),
                SelectedOrganiserId = SelectedOrganiserId,
                Events = await events.ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id, string searchString)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Organiser)
                .Include(e => e.Reservations)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

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
            var viewModel = new EventViewModel
            {
                Event = new Event(),
                Organisers = new SelectList(_context.Organizers, "Id", "Name")
            };
            return View(viewModel);
        }


        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("Validatiefouten: " + string.Join(", ", errors));

                TempData["ErrorMessage"] = "Validatie mislukt: " + string.Join(", ", errors);
            }

            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Organisers = new SelectList(_context.Organizers, "Id", "Name", viewModel.Event.OrganiserId);
            return View(viewModel);
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

            var viewModel = new EventViewModel
            {
                Event = @event,
                Organisers = new SelectList(_context.Organizers, "Id", "Name", @event.OrganiserId)
            };

            return View(viewModel);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventViewModel viewModel)
        {
            if (id != viewModel.Event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(viewModel.Event.Id))
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

            viewModel.Organisers = new SelectList(_context.Organizers, "Id", "Name", viewModel.Event.OrganiserId);
            return View(viewModel);
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
                var reservaties = _context.Reservaties.Where(r => r.EventID == id);
                _context.Reservaties.RemoveRange(reservaties);

                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        // POST: Toggle Payment Status
        [HttpPost]
        public IActionResult TogglePaymentStatus(int id)
        {
            var reservatie = _context.Reservaties.Find(id);

            if (reservatie == null)
            {
                TempData["ErrorMessage"] = "Reservatie niet gevonden.";
                return RedirectToAction("Index", "Home");
            }

            reservatie.Paid = !reservatie.Paid;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "De betaalstatus is succesvol bijgewerkt.";
            return RedirectToAction("Details", new { id = reservatie.EventID });
        }

        // POST: Reservering maken
        [HttpPost]
        public IActionResult Reserve(string Name, int EventID, string Email, string Type, int Amount)
        {
            int price = Type switch
            {
                "Backstage" => Amount * 45,
                "VIP" => Amount * 30,
                _ => Amount * 15,
            };

            var eventObj = _context.Events.Find(EventID);
            if (eventObj == null)
            {
                TempData["ErrorMessage"] = "Het evenement kon niet worden gevonden.";
                return RedirectToAction("Index", "Home");
            }

            _context.Reservaties.Add(new Reservatie
            {
                Name = Name,
                Email = Email,
                ReservationNumber = new Random().Next(100000, 999999),
                Date = DateTime.Now,
                Type = Type,
                Amount = Amount,
                Paid = false,
                Price = price,
                EventID = EventID
            });

            if (ModelState.IsValid)
            {
                eventObj.CurrentParticipants += Amount;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Je reservering is succesvol geplaatst U kunt het openstaande bedrag ter plekke betalen:" + price;
                return RedirectToAction("Index", "Home");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            Console.WriteLine("Validatiefouten: " + string.Join(", ", errors));

            return RedirectToAction("Index", "Home");
        }
    }
}
