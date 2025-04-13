using System.ComponentModel.DataAnnotations;

namespace TicketsCinema.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}