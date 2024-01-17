using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "Wartość {0} musi mieć co najmniej {2} i maksymalnie {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Kod uwierzytelniający")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Zapamiętaj tę maszynę")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
