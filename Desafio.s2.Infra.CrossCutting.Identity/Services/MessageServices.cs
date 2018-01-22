using System.Threading.Tasks;

namespace Desafio.s2.Infra.CrossCutting.Identity.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(EmailSettings emailSettings)
        {
            throw new System.NotImplementedException();
        }

        public Task SendSmsAsync(string number, string message)
        { 
            //todo :  send sms here
            return Task.FromResult(0);
        }
    }
}