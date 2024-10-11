using System;
using System.ComponentModel.DataAnnotations;

namespace Eventinleveropdracht.Models
{
    public class Reservatie
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }

        public int? ReservationNumber { get; set; }

        public DateTime? Date { get; set; } = DateTime.Now;

        public string? Type { get; set; }

        public int? Amount { get; set; }

        public bool Paid { get; set; }

        public int? Price { get; set; }

        public Event? Event { get; set; }
        public int? EventID { get; set; }
    }
}
