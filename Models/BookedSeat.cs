﻿namespace TicketsCinema.Models
{
    public class BookedSeat
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int SeatId { get; set; }
        public Seat Seat { get; set; } = null!;

        public double PriceBooked { get; set; }
    }
}
