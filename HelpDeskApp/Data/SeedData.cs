using HelpDeskApp.Helpers;
using HelpDeskApp.Models;
using HelpDeskApp.Models.UserAccountViewModel;

namespace HelpDeskApp.Data
{
    public class SeedData
    {
        public IEnumerable<ComplaintCategory> GetComplaintCategoryList()
        {
            return new List<ComplaintCategory>
            {
                new ComplaintCategory { Name = "Software", Description = "Software"},
                new ComplaintCategory { Name = "Hardware", Description = "Hardware"},
                new ComplaintCategory { Name = "Sieć", Description = "Sieć"},
                new ComplaintCategory { Name = "Finanse", Description = "Finanse"},
                new ComplaintCategory { Name = "Marketing", Description = "Marketing"},
                new ComplaintCategory { Name = "HR", Description = "HR"},
                new ComplaintCategory { Name = "Default", Description = "Default"},
            };
        }

        public IEnumerable<ComplaintStatus> GetComplaintStatusList()
        {
            return new List<ComplaintStatus>
            {
                new ComplaintStatus { Name = "Nowe", Description = "Nowe zgłoszenie"},
                new ComplaintStatus { Name = "Otwarte", Description = "Otwarte zgłoszenie"},
                new ComplaintStatus { Name = "W trakcie", Description = "Zgłoszenie w toku"},
                new ComplaintStatus { Name = "Rozwiązane", Description = "Zgłoszenie rozwiązane"},
                new ComplaintStatus { Name = "Odrzucone", Description = "Zgłoszenie odrzucone"},
                new ComplaintStatus { Name = "Zablokowane", Description = "Zgłoszenie zablokowane"},
                new ComplaintStatus { Name = "Zamknięte", Description = "Zgłoszenie zamknięte"},
            };
        }

        public IEnumerable<Priority> GetPriorityList()
        {
            return new List<Priority>
            {
                new Priority { Name = "Wysokie", Description = "Wysoki priorytet"},
                new Priority { Name = "Niski", Description = "Niski priorytet"},
                new Priority { Name = "Średni", Description = "Średni priorytet"},

                new Priority { Name = "Krytyczny", Description = "Krytyczny priorytet"},
                new Priority { Name = "Umiarkowany", Description = "Umiarkowany priorytet"},
                new Priority { Name = "Główny", Description = "Główny Major"},
                new Priority { Name = "Inny", Description = "Inny priorytet"},
            };
        }
        public IEnumerable<Complaint> GetComplaintList()
        {
            return new List<Complaint>
            {
                new Complaint { Name = "Testowe Zgłoszenie", Description = "TBD", Category = 1, Status = 1, Priority = 1 },
                new Complaint { Name = "Testowe Zgłoszenie", Description = "TBD", Category = 1, Status = 1, Priority = 1 },
                new Complaint { Name = "Testowe Zgłoszenie", Description = "TBD", Category = 1, Status = 1, Priority = 1 },
                new Complaint { Name = "Testowe Zgłoszenie", Description = "TBD", Category = 2, Status = 1, Priority = 1 },
                new Complaint { Name = "Testowe Zgłoszenie", Description = "TBD", Category = 2, Status = 1, Priority = 1 },

                new Complaint { Name = "Testowe Zgłoszenie", Description = "TBD", Category = 3, Status = 3, Priority = 2 },
                new Complaint { Name = "Testowe Zgłoszenie", Description = "TBD", Category = 3, Status = 3, Priority = 2 },

            };
        }
        public IEnumerable<UserProfileCRUDViewModel> GetUserProfileList()
        {
            return new List<UserProfileCRUDViewModel>
            {
                new UserProfileCRUDViewModel { FirstName = "Shop5", LastName = "User", Email = "Shop5@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U1.png", Address = "Kraków", Country = "Polska" },
                new UserProfileCRUDViewModel { FirstName = "Shop4", LastName = "User", Email = "Shop4@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U2.png", Address = "Kraków", Country = "Polska"  },
                new UserProfileCRUDViewModel { FirstName = "Shop3", LastName = "User", Email = "Shop3@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U3.png", Address = "Kraków", Country = "Polska"  },
                new UserProfileCRUDViewModel { FirstName = "Shop2", LastName = "User", Email = "Shop2@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U4.png", Address = "Kraków", Country = "Polska"  },
                new UserProfileCRUDViewModel { FirstName = "Shop1", LastName = "User", Email = "Shop1@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U5.png", Address = "Kraków", Country = "Polska"  },

                new UserProfileCRUDViewModel { FirstName = "Accountants1", LastName = "User", Email = "accountants1@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U6.png", Address = "Kraków", Country = "Polska" },
                new UserProfileCRUDViewModel { FirstName = "Accountants2", LastName = "User", Email = "accountants2@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U7.png", Address = "Kraków", Country = "Polska" },
                new UserProfileCRUDViewModel { FirstName = "Accountants3", LastName = "User", Email = "accountants3@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U8.png", Address = "Kraków", Country = "Polska" },
                new UserProfileCRUDViewModel { FirstName = "Accountants4", LastName = "User", Email = "accountants4@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U9.png", Address = "Kraków", Country = "Polska" },
                new UserProfileCRUDViewModel { FirstName = "Accountants5", LastName = "User", Email = "accountants5@gmail.com", PasswordHash = "123", ConfirmPassword = "123", PhoneNumber= StaticData.RandomDigits(9), ProfilePicturePath = "/images/DefaultUser/U10.png", Address = "Kraków", Country = "Polska" },
            };
        }
        public CompanyInfo GetCompanyInfo()
        {
            return new CompanyInfo
            {
                Name = "Testowa Firma",
                ApplicationTitle = "HelpDeskApp",
                CompanyLogoImagePath = "/upload/blank_logo.png",
                Currency = "zł",
                Address = "Lachowice, Polska",
                City = "Lachowice",
                Country = "Polska",
                Phone = "132546789",
                Fax = "9999",
                Email = "XYZ@GMAIL.COM",
                Website = "www.wyx.com",
            };
        }
    }
}