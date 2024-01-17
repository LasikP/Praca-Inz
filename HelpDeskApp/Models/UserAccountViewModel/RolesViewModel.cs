using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.UserAccountViewModel
{
    public class RolesViewModel
    {
        [Display(Name = "SL")]
        public int Id { get; set; }
        [Display(Name = "Nazwa roli")]
        public string Name { get; set; }
        public string RoleID { get; set; }
    }
}
