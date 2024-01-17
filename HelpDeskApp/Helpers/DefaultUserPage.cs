using HelpDeskApp.Pages;

namespace HelpDeskApp.Helpers
{
    public static class DefaultUserPage
    {
        public static readonly string[] PageCollection =
            {
                MainMenu.Dashboard.PageName,
                MainMenu.UserProfile.PageName,
                MainMenu.MyComplaint.PageName,
                MainMenu.AssignToMe.PageName,
                MainMenu.Notification.PageName
            };
    }
}
