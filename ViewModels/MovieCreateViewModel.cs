namespace TicketsCinema.ViewModels
{
    public class MovieCreateViewModel
    {
        public string Title { get; set; }
        public string? PreviewUrl { get; set; }
        public string? ShortDesc { get; set; }
        public DateTime? DateTime { get; set; }
        public double Price { get; set; }
    }
}