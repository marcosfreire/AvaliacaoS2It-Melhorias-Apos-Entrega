namespace Desafio.s2.Infra.CrossCutting.Identity.Authorization
{
    public class TokenDescriptor
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }        
        public int MinutesValid { get; set; }
    }
}