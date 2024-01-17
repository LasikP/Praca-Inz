using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Imię")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Numer telefonu")]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Display(Name = "Kraj")]
        public string Country { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Wartość {0} musi mieć co najmniej {2} i maksymalnie {1} znaków.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz Hasło")]
        [Compare("Password", ErrorMessage = "Hasło i hasło potwierdzające nie są zgodne.")]
        public string ConfirmPassword { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
