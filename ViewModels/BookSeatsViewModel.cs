using System.Collections.Generic;

namespace TicketsCinema.Models
{
    public class BookSeatsViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public List<int> SelectedSeatIds { get; set; } // Список выбранных мест
        public List<Seat> AvailableSeats { get; set; } // Доступные места
        public List<Seat> AllSeats { get; set; } // Доступные места
        public BookSeatsViewModel()
        {
            SelectedSeatIds = new List<int>();
            AvailableSeats = new List<Seat>();
            AllSeats = new List<Seat>();
        }
    }
}