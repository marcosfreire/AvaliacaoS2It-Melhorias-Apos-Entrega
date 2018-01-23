using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Desafio.s2.Infra.CrossCutting.Identity.Services;
using Desafio.s2.App.Service.ViewModels;
using System;

namespace Desafio.s2.Site.Services
{
    public static class EmailSenderExtensions
    {
        public static async Task EnviarEmailConfiirmacaoEmailAsync(this IEmailSender emailSender, EmailSettings emailSettings, string callbackUrl)
        {
            await Task.Factory.StartNew(() =>
           {
               emailSettings.Subject = "Confirme seu email";
               emailSettings.Body = $"Por favor , confirme seu email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui!</a>";

               return emailSender.SendEmailAsync(emailSettings);
           });
        }

        public static async Task EnviarEmailEsqueciMinhaSenhaAsync(this IEmailSender emailSender, EmailSettings emailSettings, string callbackUrl)
        {
            await Task.Factory.StartNew(() =>
            {
                emailSettings.Subject = "Resetar senha";
                emailSettings.Body = $"Por favor , resete sua senha <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui!</a>";

                return emailSender.SendEmailAsync(emailSettings);
            });
        }

        #region SolicitarDevolucaoJogo

        public static async Task EnviarEmailCobrancaJogoAsync(this IEmailSender emailSender, EmailSettings emailSettings, JogoViewModel jogoViewModel)
        {
            await Task.Factory.StartNew(() =>
            {
                emailSettings.Subject = $"Devolução Jogo - {jogoViewModel.Nome}";
                emailSettings.ToEmail = jogoViewModel.EmprestadoPara.Email;
                
                emailSettings.Body = CriarCorpoEmailSolicitarDevolucao(jogoViewModel);
                emailSettings.FromEmail = emailSettings.FromEmail;

                emailSender.SendEmailAsync(emailSettings);

                return emailSender.SendEmailAsync(emailSettings);
            });
        }

        private static string CriarCorpoEmailSolicitarDevolucao(JogoViewModel jogoViewModel)
        {
            var body = $"Olá {jogoViewModel.EmprestadoPara.Nome} !<p>Favor devolver o jogo {jogoViewModel.Nome} ;]</p>";
            
            body += "<p>Se o jogo já foi devolvido , favor ignorar esta mensagem</p><p>Desde já muito obrigado!</p>";

            return body;
        }
        
        private static bool PossuiImagem(JogoViewModel jogoViewModel)
        {
            return !string.IsNullOrEmpty(jogoViewModel.ThumbnailCapaJogo);
        }

        #endregion
    }
}