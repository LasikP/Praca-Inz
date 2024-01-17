using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.UserAccountViewModel
{
    public class UserProfileCRUDViewModel : EntityBase
    {
        [Display(Name = "Identyfikator profilu użytkownika")]
        public Int64 UserProfileId { get; set; }

        public string ApplicationUserId { get; set; }

        [Display(Name = "Imię")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Numer telefonu")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Adres Email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        [Display(Name = "Zdjęcie profilowe")]
        public IFormFile ProfilePicture { get; set; }
        public string ProfilePicturePath { get; set; } = "/images/DefaultUser/user.png";
        public string PasswordHash { get; set; }
        [Display(Name = "Potwierdź hasło")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Hasło i hasło potwierdzające nie są zgodne.")]
        public string ConfirmPassword { get; set; }
        public string CurrentURL { get; set; }


        public static implicit operator UserProfileCRUDViewModel(UserProfile _UserProfile)
        {
            return new UserProfileCRUDViewModel
            {
                UserProfileId = _UserProfile.UserProfileId,
                ApplicationUserId = _UserProfile.ApplicationUserId,
                FirstName = _UserProfile.FirstName,
                LastName = _UserProfile.LastName,
                PhoneNumber = _UserProfile.PhoneNumber,
                Email = _UserProfile.Email,
                Address = _UserProfile.Address,
                Country = _UserProfile.Country,

                CreatedDate = _UserProfile.CreatedDate,
                ModifiedDate = _UserProfile.ModifiedDate,
                CreatedBy = _UserProfile.CreatedBy,
                ModifiedBy = _UserProfile.ModifiedBy,
                ProfilePicturePath = _UserProfile.ProfilePicture
            };
        }

        public static implicit operator UserProfile(UserProfileCRUDViewModel vm)
        {
            return new UserProfile
            {
                UserProfileId = vm.UserProfileId,
                ApplicationUserId = vm.ApplicationUserId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.PhoneNumber,
                Email = vm.Email,
                Address = vm.Address,
                Country = vm.Country,

                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                ProfilePicture = vm.ProfilePicturePath
            };
        }
    }
}
