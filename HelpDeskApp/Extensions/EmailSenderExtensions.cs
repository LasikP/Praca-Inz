using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HelpDeskApp.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Potwierdü swÛj email",
                $"Potwierdü swoje konto, klikajπc ten link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
