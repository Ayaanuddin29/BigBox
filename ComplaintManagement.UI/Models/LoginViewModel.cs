namespace ComplaintManagement.UI.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class JwtResponse
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
    }
}
