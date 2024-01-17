using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.AccountViewModels
{
    public class LoginViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zaoamiętać?")]
        public bool RememberMe { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
