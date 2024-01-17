using HelpDeskApp.Models.CommonViewModel;
using System.Threading.Tasks;

namespace HelpDeskApp.Services
{
    public interface IRoles
    {
        Task GenerateRolesFromPagesAsync();

        Task AddToRoles(string applicationUserId);
        Task<MainMenuViewModel> RolebaseMenuLoad(string applicationUserId);
    }
}
