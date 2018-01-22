using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Desafio.s2.Infra.CrossCutting.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailSettings emailSettings);
    }

    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(EmailSettings emailSettings)
        {
            return Task.Run(() =>
            {
                var contaEmail = emailSettings.UsernameEmail;
                var senhaEmail = emailSettings.UsernamePassword;

                if (CredenciaisEmailNaoConfigurado(contaEmail, senhaEmail)) return Task.CompletedTask;

                try
                {
                    var smtpClient = BuildSmtpClient(emailSettings);
                    ConfigureCredentials(contaEmail, senhaEmail, smtpClient);
                    smtpClient.Send(BuildMailMessage(emailSettings, contaEmail));
                }
                catch (Exception ex)
                {
                    // todo : Implement log here
                    throw ex;
                }

                return Task.CompletedTask;
            });
        }
        
        private static void ConfigureCredentials(string contaEmail, string senhaEmail, SmtpClient smtpClient)
        {
            var credentials = new NetworkCredential(contaEmail, senhaEmail);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = credentials;
        }

        private static SmtpClient BuildSmtpClient(EmailSettings emailSettings)
        {
            return new SmtpClient(emailSettings.Domain)
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
        }

        private static MailMessage BuildMailMessage(EmailSettings emailSettings, string contaEmail)
        {
            return new MailMessage(contaEmail, emailSettings.ToEmail)
            {
                Subject = emailSettings.Subject,
                Body = emailSettings.Body,
                IsBodyHtml = true
            };
        }

        private static bool CredenciaisEmailNaoConfigurado(string contaEmail, string senhaEmail) => string.IsNullOrEmpty(contaEmail) || string.IsNullOrEmpty(senhaEmail);
    }
}