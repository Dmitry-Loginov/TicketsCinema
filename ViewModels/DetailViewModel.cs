using TicketsCinema.Models;

namespace TicketsCinema.ViewModels
{
    public class DetailViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public double Budget { get; set; }
        public List<BookedSeat> PastTickets { get; set; } = new();
        public List<BookedSeat> UpcomingTickets { get; set; } = new();
    }
}
