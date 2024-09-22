namespace Eventinleveropdracht.Models
{
    public class Guest : User
    {
        public List<Order> Orders { get; set; }
        public List<Reservatie> Reservaties { get; set; }
    }
}
