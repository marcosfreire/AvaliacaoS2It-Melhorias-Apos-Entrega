namespace Desafio.s2.Infra.CrossCutting.Identity.Services
{
    public class EmailSettings
    {
        public string Subject { get; set; }

        public string Domain { get; set; }

        public int Port { get; set; }
        
        public string UsernameEmail { get; set; }

        public string UsernamePassword { get; set; }

        public string FromEmail { get; set; }

        public string ToEmail { get; set; }

        public string Link { get; set; }

        public string Body { get; set; }
    }
}
