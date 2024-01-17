using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Kod odzyskiwania")]
        public string RecoveryCode { get; set; }
    }
}
