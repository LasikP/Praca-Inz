﻿using System.Linq;
using System.Threading.Tasks;
using HelpDeskApp.Services;

namespace HelpDeskApp.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, IFunctional functional)
        {
            context.Database.EnsureCreated();
            if (context.ApplicationUser.Any())
            {
                return;
            }
            else
            {
                functional.InitAppData();
                await functional.CreateDefaultSuperAdmin();
                await functional.CreateDefaultOtherUser();
                await functional.CreateDefaultEmailSettings();
                await functional.CreateDefaultIdentitySettings();
            }
        }
    }
}
