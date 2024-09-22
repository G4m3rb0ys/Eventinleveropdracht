using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventinleveropdracht.Models
{
    public class Reservatie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int ReservationNumber { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string type { get; set; }
        public int ammount { get; set; }
        public bool Paid { get; set; }
        public int Price { get; set; }
        public Event Event { get; set; }
        public int EventID { get; set; }
        public Guest Guest { get; set; }
        public int GuestId { get; set; }
    }
}
