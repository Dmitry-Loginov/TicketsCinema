namespace TicketsCinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? PreviewUrl { get; set; }
        public string? ShortDesc { get; set; }
        public DateTime? DateTime { get; set; }
        public double Price { get; set; }
        public string? Title { get; set; }

        public ICollection<BookedSeat> BookedSeats { get; set; } = new LinkedList<BookedSeat>();
    }
}
