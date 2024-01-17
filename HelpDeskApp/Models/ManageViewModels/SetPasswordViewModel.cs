using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.ManageViewModels
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Wartość {0} musi mieć co najmniej {2} i maksymalnie {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i hasło potwierdzające nie są zgodne.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
