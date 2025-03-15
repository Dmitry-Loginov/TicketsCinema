using System.ComponentModel.DataAnnotations;

namespace TicketsCinema.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}