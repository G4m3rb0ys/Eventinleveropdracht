using Eventinleveropdracht.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Zorg dat deze aanwezig is
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Eventinleveropdracht.Data; // Zorg dat de juiste namespace wordt toegevoegd

namespace Eventinleveropdracht.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VoorbeeldDatabase _context; // Gebruik jouw DbContext: VoorbeeldDatabase

        public HomeController(ILogger<HomeController> logger, VoorbeeldDatabase context)
        {
            _logger = logger;
            _context = context; // Injecteer de juiste context
        }

        public async Task<IActionResult> Index()
        {
            // Haal alle aankomende evenementen op
            var upcomingEvents = await _context.Events
                .Where(e => e.FromDate >= DateTime.Now) // Filter op toekomstige evenementen
                .OrderBy(e => e.FromDate) // Sorteer op startdatum
                .ToListAsync(); // Asynchrone oproep om de resultaten op te halen

            return View(upcomingEvents); // Stuur de lijst van evenementen naar de view
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
