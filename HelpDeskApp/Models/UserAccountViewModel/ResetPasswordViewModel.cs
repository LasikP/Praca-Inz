using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.UserAccountViewModel
{
    public class ResetPasswordViewModel
    {
        public string ApplicationUserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Stare hasło")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Wartość {0} musi mieć co najmniej {2} i maksymalnie {1} znaków.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasło i hasło potwierdzające nie są zgodne.")]
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}
