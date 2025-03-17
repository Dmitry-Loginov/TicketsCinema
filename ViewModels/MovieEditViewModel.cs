namespace TicketsCinema.ViewModels
{
    public class MovieEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? PreviewUrl { get; set; }
        public string? ShortDesc { get; set; }
        public DateTime? DateTime { get; set; }
        public double Price { get; set; }
    }
}
