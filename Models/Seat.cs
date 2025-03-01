namespace TicketsCinema.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public ICollection<BookedSeat> BookedSeats { get; set; } = new LinkedList<BookedSeat>();
    }
}
