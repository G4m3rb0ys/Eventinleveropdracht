using Eventinleveropdracht.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eventinleveropdracht.Models
{
    public class EventViewModel
    {
        public Event Event { get; set; }
        public SelectList? Organisers { get; set; }
        public int? SelectedOrganiserId { get; set; }
        public List<Event>? Events { get; set; }
    }
}
