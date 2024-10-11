using Eventinleveropdracht.Data; // Zorg ervoor dat je de juiste namespace voor je databasecontext hebt
using Eventinleveropdracht.Models; // Zorg ervoor dat je toegang hebt tot het Event model
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsListController : ControllerBase
    {
        private readonly VoorbeeldDatabase _context; // Injecteer de databasecontext

        public EventsListController(VoorbeeldDatabase context)
        {
            _context = context; // Sla de context op voor gebruik in de controller
        }

        // GET: api/<EventsListController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EventsListController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var @event = await _context.Events
                .Include(e => e.Organiser)
                .Include(e => e.Reservations) 
                .FirstOrDefaultAsync(e => e.Id == id);

            return @event; 
        }

        // POST api/<EventsListController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EventsListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventsListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
