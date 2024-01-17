using HelpDeskApp.Models;
using HelpDeskApp.Models.UserAccountViewModel;

namespace HelpDeskApp.Services
{
    public interface IAccountService
    {
        Task<Tuple<ApplicationUser, string>> CreateUserProfile(UserProfileCRUDViewModel vm, string LoginUser);       
    }
}
