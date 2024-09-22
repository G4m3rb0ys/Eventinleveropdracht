using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventinleveropdracht.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public Reservatie Reservatie { get; set; }
        public int ReservatieId { get; set; }
        public Guest Guest { get; set; }
        public int GuestId { get; set; }
    }
}
