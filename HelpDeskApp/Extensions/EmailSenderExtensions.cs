using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HelpDeskApp.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Potwierd� sw�j email",
                $"Potwierd� swoje konto, klikaj�c ten link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
