using HelpDeskApp.Data;
using HelpDeskApp.Models;
using HelpDeskApp.Models.AccountViewModels;
using HelpDeskApp.Models.CommonViewModel;
using HelpDeskApp.Models.UserAccountViewModel;
using HelpDeskApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UAParser;

namespace HelpDeskApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ICommon _iCommon;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ICommon iCommon)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
            _logger = logger;
            _iCommon = iCommon;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            //Wyczyść istniejący zewnętrzny plik cookie, aby zapewnić czysty proces logowania 
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            JsonResultViewModel _JsonResultViewModel = new();
            try
            {
                //Aby umożliwić błędom hasła wyzwalanie blokady konta, ustaw lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                await InserLoginHistory(true, result.Succeeded, model);

                string _AlertMessage = "Użytkownik zalogowany.";
                if (result.Succeeded)
                {
                    //czekaj na JWTHandle (model);
                    HttpContext.Session.SetString("LoginUserName", model.Email);
                    _logger.LogInformation(_AlertMessage);

                    _JsonResultViewModel.AlertMessage = _AlertMessage;
                    _JsonResultViewModel.IsSuccess = result.Succeeded;
                    return new JsonResult(_JsonResultViewModel);
                }
                else
                {
                    _AlertMessage = "Nieprawidłowa próba logowania.";
                    _logger.LogInformation(_AlertMessage);
                    _JsonResultViewModel.AlertMessage = _AlertMessage;
                    _JsonResultViewModel.IsSuccess = result.Succeeded;
                    return new JsonResult(_JsonResultViewModel);
                }
            }
            catch (Exception ex)
            {
                _JsonResultViewModel.IsSuccess = false;
                _JsonResultViewModel.AlertMessage = ex.Message;
                return new JsonResult(_JsonResultViewModel);
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Upewnij się, że użytkownik najpierw przeszedł przez ekran nazwy użytkownika i hasła
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Nie można wczytać użytkownika z uwierzytelnianiem dwuskładnikowym.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Nie można wczytać użytkownika z identyfikatorem '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("Użytkownik o identyfikatorze {UserId} zalogował się w 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("Konto użytkownika o identyfikatorze {UserId} zostało zablokowane.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Wprowadzono nieprawidłowy kod uwierzytelniający dla użytkownika o identyfikatorze {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Nieprawidłowy kod uwierzytelniający.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Upewnij się, że użytkownik najpierw przeszedł przez ekran nazwy użytkownika i hasła
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Nie można wczytać użytkownika z uwierzytelnianiem dwuskładnikowym..");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Nie można wczytać użytkownika z uwierzytelnianiem dwuskładnikowym.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("Użytkownik o identyfikatorze {UserId} zalogował się przy użyciu kodu odzyskiwania.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("Konto użytkownika o identyfikatorze {UserId} zostało zablokowane.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Wprowadzono nieprawidłowy kod odzyskiwania dla użytkownika o identyfikatorze {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Wprowadzono nieprawidłowy kod odzyskiwania.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var _ApplicationUser = new ApplicationUser
                {
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    Email = model.Email,
                    EmailConfirmed = false
                };
                var result = await _userManager.CreateAsync(_ApplicationUser, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(_ApplicationUser, "Dashboard");
                    await _userManager.AddToRoleAsync(_ApplicationUser, "Profil użytkownika");

                    //Insert: UserProfile
                    UserProfile _UserProfile = new UserProfile
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Address = model.Address,
                        Country = model.Country,

                        PasswordHash = _ApplicationUser.PasswordHash,
                        ConfirmPassword = _ApplicationUser.PasswordHash,
                        ApplicationUserId = _ApplicationUser.Id,

                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CreatedBy = HttpContext.User.Identity.Name,
                        ModifiedBy = HttpContext.User.Identity.Name
                    };
                    var result2 = await _context.UserProfile.AddAsync(_UserProfile);
                    _context.SaveChanges();
                    _logger.LogInformation("Użytkownik utworzył nowe konto z hasłem.");

                    var _DefaultIdentityOptions = await _context.DefaultIdentityOptions.FirstOrDefaultAsync(m => m.Id == 1);
                    if (_DefaultIdentityOptions.SignInRequireConfirmedEmail)
                    {
                        var _ConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(_ApplicationUser);
                        var callbackUrl = Url.EmailConfirmationLink(_ApplicationUser.Id, _ConfirmationToken, Request.Scheme);
                        await _emailSender.SendEmailConfirmationAsync(_ApplicationUser.Email, callbackUrl);
                    }
                    await _signInManager.SignInAsync(_ApplicationUser, isPersistent: false);
                    LoginViewModel _LoginViewModel = new LoginViewModel
                    {
                        Email = model.Email,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude
                    };
                    InserLoginHistory(true, true, _LoginViewModel);
                    _logger.LogInformation("Użytkownik utworzył nowe konto z hasłem.");
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LoginViewModel vm)
        {
            await _signInManager.SignOutAsync();
            InserLoginHistory(false, true, vm);
            _logger.LogInformation("Użytkownik się wylogował.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Poproś o przekierowanie do zewnętrznego dostawcy logowania.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Błąd od zewnętrznego dostawcy: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

           
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("Użytkownik zalogował się u dostawcy {Name}.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Błąd ładowania zewnętrznych danych logowania podczas potwierdzania.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("Użytkownik utworzył konto przy użyciu dostawcy {Name}.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Nie można wczytać użytkownika z identyfikatorem '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                var result = await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Zresetuj swoje hasło, klikając tutaj: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("Aby zresetować hasło, należy podać kod.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    
                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
                AddErrors(result);
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task InserLoginHistory(bool IsLoginAction, bool _IsSuccess, LoginViewModel _LoginViewModel)
        {
            var _Headers = HttpContext.Request.Headers["User-Agent"];
            var _Parser = Parser.GetDefault();
            ClientInfo _ClientInfo = _Parser.Parse(_Headers);

            LoginHistory vm = new LoginHistory
            {
                Browser = _ClientInfo.UA.Family,
                OperatingSystem = _ClientInfo.OS.Family,
                Device = _ClientInfo.Device.Family,
                ActionStatus = _IsSuccess == true ? "Success" : "Failed",
                Latitude = _LoginViewModel.Latitude,
                Longitude = _LoginViewModel.Longitude,
            };

            if (IsLoginAction)
            {
                vm.UserName = _LoginViewModel.Email;
                vm.LoginTime = DateTime.Now;
                vm.Duration = 0;
                vm.Action = "Login";

                vm.CreatedBy = _LoginViewModel.Email;
                vm.ModifiedBy = _LoginViewModel.Email;
            }
            else
            {
                string _UserName = HttpContext.User.Identity.Name;
                var _LoginHistory = _context.LoginHistory.Where(x => x.UserName == _UserName && x.Action == "Login").OrderByDescending(x => x.CreatedDate).Take(1).SingleOrDefault();
                vm.UserName = _UserName;
                vm.LoginTime = _LoginHistory.LoginTime; //Bug null
                vm.LogoutTime = DateTime.Now;
                vm.Duration = (DateTime.Now - _LoginHistory.LoginTime).TotalMinutes;
                vm.Action = "Logout";

                vm.CreatedBy = _UserName;
                vm.ModifiedBy = _UserName;
            }
            await _iCommon.InsertLoginHistory(vm, _ClientInfo);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
            }
        }

        #endregion
    }
}
