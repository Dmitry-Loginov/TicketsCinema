﻿using Microsoft.AspNetCore.Identity;

namespace TicketsCinema.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public double Budget { get; set; }

        public ICollection<BookedSeat> BookedSeats { get; set; } = new LinkedList<BookedSeat>();
    }
}
