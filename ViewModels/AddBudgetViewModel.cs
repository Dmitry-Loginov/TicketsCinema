using System.ComponentModel.DataAnnotations;

namespace TicketsCinema.ViewModels
{
    public class AddBudgetViewModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Сумма должна быть положительной.")]
        public double Amount { get; set; }
        public string? UserId { get; set; } = null;
    }
}