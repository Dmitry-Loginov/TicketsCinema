using Microsoft.EntityFrameworkCore;

namespace TicketsCinema.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Seat> Seats { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<BookedSeat> BookedSeats { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка отношений для BookedSeat
            modelBuilder.Entity<BookedSeat>()
                .HasKey(bs => bs.Id); // Уникальный ключ для BookedSeat

            //modelBuilder.Entity<BookedSeat>()
            //    .HasOne(bs => bs.User)
            //    .WithMany(u => u.BookedSeats)
            //    .HasForeignKey(bs => bs.UserId)
            //    .OnDelete(DeleteBehavior.Cascade); // Опционально: поведение при удалении

            modelBuilder.Entity<BookedSeat>()
                .HasOne(bs => bs.Movie)
                .WithMany(m => m.BookedSeats)
                .HasForeignKey(bs => bs.MovieId)
                .OnDelete(DeleteBehavior.Cascade); // Опционально: поведение при удалении

            modelBuilder.Entity<BookedSeat>()
                .HasOne(bs => bs.Seat)
                .WithMany(s => s.BookedSeats)
                .HasForeignKey(bs => bs.SeatId)
                .OnDelete(DeleteBehavior.Cascade); // Опционально: поведение при удалении
        }
    }
}
