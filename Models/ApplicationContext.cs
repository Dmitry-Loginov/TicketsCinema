using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketsCinema.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Seat> Seats { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<BookedSeat> BookedSeats { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка отношений для BookedSeat
            modelBuilder.Entity<BookedSeat>()
                .HasKey(bs => bs.Id); // Уникальный ключ для BookedSeat

            modelBuilder.Entity<BookedSeat>()
                .HasOne(bs => bs.User)
                .WithMany(u => u.BookedSeats)
                .HasForeignKey(bs => bs.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Опционально: поведение при удалении

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

            modelBuilder.Entity<Seat>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Seat>().HasData(GenerateSeats());

        }

        private static Seat[] GenerateSeats()
        {
            var seats = new Seat[214];
            for (int i = 0; i < 214; i++)
            {
                seats[i] = new Seat { Id = i + 1 }; // Ид от 1 до 214
            }
            return seats;
        }
    }
}
