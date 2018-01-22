using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Desafio.s2.Infra.CrossCutting.Identity.Services;

namespace Desafio.s2.Site.Services
{
    public static class EmailSenderExtensions
    {
        public static async Task SendEmailConfirmationAsync(this IEmailSender emailSender,EmailSettings emailSettings , string callbackUrl)
        {
            await Task.Factory.StartNew(() =>
           {
               emailSettings.Subject = "Confirme seu email";
               emailSettings.Body = $"Por favor , confirme seu email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui!</a>";

               return emailSender.SendEmailAsync(emailSettings);
           }); 
        }

        public static async Task SendEmailForgotPasswordAsync(this IEmailSender emailSender,EmailSettings emailSettings , string callbackUrl)
        {   
            await Task.Factory.StartNew(() =>
            {
                emailSettings.Subject = "Resetar senha";
                emailSettings.Body = $"Por favor , resete sua senha <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui!</a>";

                return emailSender.SendEmailAsync(emailSettings);
            });
        }
    }
}