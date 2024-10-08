using Eventinleveropdracht.Models;
using System.ComponentModel.DataAnnotations;

namespace Eventinleveropdracht.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }

        public List<string> Requirements { get; set; }
        public int MaxParticipants { get; set; }
        public int? CurrentParticipants { get; set; }
        public string? Image { get; set; }
        public List<Reservatie>? Reservations { get; set; }

        public int? OrganiserId { get; set; }
        public Organizer? Organiser { get; set; }
    }
}