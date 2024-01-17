using Microsoft.AspNetCore.Identity;
using HelpDeskApp.Data;
using HelpDeskApp.Models;
using HelpDeskApp.Services;

namespace HelpDeskApp.Helpers
{
    public static class ProgramTaskExtension
    {
        public static void SeedingData(WebApplication app)
        {
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();
                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                        var functional = services.GetRequiredService<IFunctional>();

                        DbInitializer.Initialize(context, functional).Wait();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
