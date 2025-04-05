using System.Security.Cryptography.X509Certificates;

namespace Mockify.API.DTO
{
    public class UserToken
    {
        public UserToken(string email, string token)
        {
            this.Email = email;
            this.Token = token;
        }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
