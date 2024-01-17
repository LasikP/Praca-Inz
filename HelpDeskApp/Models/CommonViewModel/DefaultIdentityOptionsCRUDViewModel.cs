using System.ComponentModel.DataAnnotations;

namespace HelpDeskApp.Models.CommonViewModel
{
    public class DefaultIdentityOptionsCRUDViewModel : EntityBase
    {
        //password settings
        public int Id { get; set; }
        [Display(Name = "Hasło Wymaga cyfry")]
        public bool PasswordRequireDigit { get; set; }
        [Display(Name = "Wymagana długość hasła")]
        public int PasswordRequiredLength { get; set; }
        [Display(Name = "Wymagaj znaków innych niż alfanumeryczne Hasło")]
        public bool PasswordRequireNonAlphanumeric { get; set; }
        [Display(Name = "Hasło wymaga wielkich liter")]
        public bool PasswordRequireUppercase { get; set; }
        [Display(Name = "Hasło wymaga małych liter")]
        public bool PasswordRequireLowercase { get; set; }
        [Display(Name = "Hasło wymafa unikalnych znaków")]
        public int PasswordRequiredUniqueChars { get; set; }

        //lockout settings
        [Display(Name = "Domyślny czas blokady w minutach")]
        public double LockoutDefaultLockoutTimeSpanInMinutes { get; set; }
        [Display(Name = "Blokada Maksymalna, liczba nieudanych prób dostępu")]
        public int LockoutMaxFailedAccessAttempts { get; set; }
        [Display(Name = "Blokada dozwolona dla nowych użytkowników")]
        public bool LockoutAllowedForNewUsers { get; set; }

        //user settings
        [Display(Name = "Użytkownik wymaga unikalnego adresu e-mail")]
        public bool UserRequireUniqueEmail { get; set; }
        [Display(Name = "Zaloguj się Wymagaj potwierdzenia adresu e-mail")]
        public bool SignInRequireConfirmedEmail { get; set; }

        //cookie settings
        [Display(Name = "Tylko plik cookie HTTP")]
        public bool CookieHttpOnly { get; set; }
        [Display(Name = "Wygaśnięcie pliku cookie")]
        public double CookieExpiration { get; set; }
        [Display(Name = "Okres ważności plików cookie")]
        public double CookieExpireTimeSpan { get; set; }
        [Display(Name = "Ścieżka logowania")]
        public string LoginPath { get; set; }
        [Display(Name = "Ścieżka wylogowania")]
        public string LogoutPath { get; set; }
        [Display(Name = "Ścieżka odmowy dostępu")]
        public string AccessDeniedPath { get; set; }
        [Display(Name = "Przesuwne wygaśnięcie")]
        public bool SlidingExpiration { get; set; }


        public static implicit operator DefaultIdentityOptionsCRUDViewModel(DefaultIdentityOptions _DefaultIdentityOptions)
        {
            return new DefaultIdentityOptionsCRUDViewModel
            {
                Id = _DefaultIdentityOptions.Id,
                PasswordRequireDigit = _DefaultIdentityOptions.PasswordRequireDigit,
                PasswordRequiredLength = _DefaultIdentityOptions.PasswordRequiredLength,
                PasswordRequireNonAlphanumeric = _DefaultIdentityOptions.PasswordRequireNonAlphanumeric,
                PasswordRequireUppercase = _DefaultIdentityOptions.PasswordRequireUppercase,
                PasswordRequireLowercase = _DefaultIdentityOptions.PasswordRequireLowercase,
                PasswordRequiredUniqueChars = _DefaultIdentityOptions.PasswordRequiredUniqueChars,
                LockoutDefaultLockoutTimeSpanInMinutes = _DefaultIdentityOptions.LockoutDefaultLockoutTimeSpanInMinutes,
                LockoutMaxFailedAccessAttempts = _DefaultIdentityOptions.LockoutMaxFailedAccessAttempts,
                LockoutAllowedForNewUsers = _DefaultIdentityOptions.LockoutAllowedForNewUsers,
                UserRequireUniqueEmail = _DefaultIdentityOptions.UserRequireUniqueEmail,
                SignInRequireConfirmedEmail = _DefaultIdentityOptions.SignInRequireConfirmedEmail,
                CookieHttpOnly = _DefaultIdentityOptions.CookieHttpOnly,
                CookieExpiration = _DefaultIdentityOptions.CookieExpiration,
                CookieExpireTimeSpan = _DefaultIdentityOptions.CookieExpireTimeSpan,
                LoginPath = _DefaultIdentityOptions.LoginPath,
                LogoutPath = _DefaultIdentityOptions.LogoutPath,
                AccessDeniedPath = _DefaultIdentityOptions.AccessDeniedPath,
                SlidingExpiration = _DefaultIdentityOptions.SlidingExpiration,
                CreatedDate = _DefaultIdentityOptions.CreatedDate,
                ModifiedDate = _DefaultIdentityOptions.ModifiedDate,
                CreatedBy = _DefaultIdentityOptions.CreatedBy,
                ModifiedBy = _DefaultIdentityOptions.ModifiedBy,
                Cancelled = _DefaultIdentityOptions.Cancelled,

            };
        }

        public static implicit operator DefaultIdentityOptions(DefaultIdentityOptionsCRUDViewModel vm)
        {
            return new DefaultIdentityOptions
            {
                Id = vm.Id,
                PasswordRequireDigit = vm.PasswordRequireDigit,
                PasswordRequiredLength = vm.PasswordRequiredLength,
                PasswordRequireNonAlphanumeric = vm.PasswordRequireNonAlphanumeric,
                PasswordRequireUppercase = vm.PasswordRequireUppercase,
                PasswordRequireLowercase = vm.PasswordRequireLowercase,
                PasswordRequiredUniqueChars = vm.PasswordRequiredUniqueChars,
                LockoutDefaultLockoutTimeSpanInMinutes = vm.LockoutDefaultLockoutTimeSpanInMinutes,
                LockoutMaxFailedAccessAttempts = vm.LockoutMaxFailedAccessAttempts,
                LockoutAllowedForNewUsers = vm.LockoutAllowedForNewUsers,
                UserRequireUniqueEmail = vm.UserRequireUniqueEmail,
                SignInRequireConfirmedEmail = vm.SignInRequireConfirmedEmail,
                CookieHttpOnly = vm.CookieHttpOnly,
                CookieExpiration = vm.CookieExpiration,
                CookieExpireTimeSpan = vm.CookieExpireTimeSpan,
                LoginPath = vm.LoginPath,
                LogoutPath = vm.LogoutPath,
                AccessDeniedPath = vm.AccessDeniedPath,
                SlidingExpiration = vm.SlidingExpiration,
                CreatedDate = vm.CreatedDate,
                ModifiedDate = vm.ModifiedDate,
                CreatedBy = vm.CreatedBy,
                ModifiedBy = vm.ModifiedBy,
                Cancelled = vm.Cancelled,

            };
        }
    }
}
