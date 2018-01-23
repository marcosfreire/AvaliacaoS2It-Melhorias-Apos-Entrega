using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Desafio.s2.Infra.CrossCutting.Identity.Models
{
    public class SigningCredentialsConfiguration
    {
        private const string SecretKey = "mysecretkey@desafios2it";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
